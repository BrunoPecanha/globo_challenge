using Pecanha.Domain.Commands;
using Pecanha.Domain.Entity;
using Pecanha.Domain.Enum;
using System;
using Xunit;

namespace Pecanha.Teste {
    public class SceneTest {
        [Fact]
        [Trait("Name", "Demon Slayer")]
        public void Alter_Pendente_To_Preparada_Op_Allowed() {
            var scene = new Scene("Demon Slayer");

            var updatecommand = new SceneUpdateCommand() {
                Id = 1,
                NextState = StateEnum.Preparada,
                OperationHour = DateTime.Now
            };

            scene.UpdateState(updatecommand);
            Assert.True(scene.Erro == ErroEnum.NoError);
        }

        [Theory]
        [InlineData(StateEnum.Preparada)]
        [InlineData(StateEnum.Pendurada)]
        [Trait("Name", "Onimusha")]
        public void Alter_Pendente_To_Preparada_And_Pendurada_Op_Allowed(StateEnum value) {
            var scene = new Scene("Onimusha");           
            SceneUpdateCommand updatecommand = new SceneUpdateCommand() {
                Id = 1,
                NextState = value,
                OperationHour = DateTime.Now
            };                   

            scene.UpdateState(updatecommand);
            Assert.True(scene.Erro == ErroEnum.NoError);
        }

        [Theory]
        [InlineData(StateEnum.Gravada)]
        [InlineData(StateEnum.Pendurada)]
        [Trait("Name", "God of War")]
        public void Alter_Preparada_To_Gravada_And_Pendurada_Op_Allowed(StateEnum value) {
            var scene = new Scene("God of War");
            SceneUpdateCommand updatecommand = new SceneUpdateCommand() {
                Id = 1,
                NextState = StateEnum.Preparada,
                OperationHour = DateTime.Now
            };
            scene.UpdateState(updatecommand);

            updatecommand = new SceneUpdateCommand() {
                Id = 1,
                NextState = value,
                OperationHour = DateTime.Now
            };
            scene.UpdateState(updatecommand);
                       
            Assert.True(scene.Erro == ErroEnum.NoError);
        }

        [Fact]
        [Trait("Name", "Stalone Cobra")]
        public void Alter_Pendente_To_Gravada_Op_Not_Allowed() {
            var scene = new Scene("DBZ");

            var updatecommand = new SceneUpdateCommand() {
                Id = 1,
                NextState = StateEnum.Gravada,
                OperationHour = DateTime.Now
            };

            scene.UpdateState(updatecommand);
            Assert.True(scene.Erro == ErroEnum.OpNotAllowed);
        }

        [Theory]
        [InlineData(StateEnum.Gravada)]
        [InlineData(StateEnum.Pendente)]
        [Trait("Name", "Os Sete Pecados Capitais")]
        public void Alter_Pendurada_To_Gravada_And_Pendente_Op_Not_Allowed(StateEnum value) {
            var scene = new Scene("Os Sete Pecados Capitais");
            SceneUpdateCommand updatecommand = new SceneUpdateCommand() {
                Id = 1,
                NextState = StateEnum.Pendurada,
                OperationHour = DateTime.Now
            };
            
            scene.UpdateState(updatecommand);
            Assert.True(scene.Erro == ErroEnum.NoError);

            // Início teste
            updatecommand = new SceneUpdateCommand() {
                Id = 1,
                NextState = value,
                OperationHour = DateTime.Now
            };

            scene.UpdateState(updatecommand);
            Assert.True(scene.Erro == ErroEnum.OpNotAllowed);
        }

        [Fact]    
        [Trait("Name", "One Punch Man")]
        public void Undo_Alter_After_5_Minutes_Op_Not_Allowed() {
            var scene = new Scene("One Punch Man");
            SceneUpdateCommand updatecommand = new SceneUpdateCommand() {
                Id = 1,
                NextState = StateEnum.Preparada,
                OperationHour = DateTime.Now
            };

            scene.UpdateState(updatecommand);
            Assert.True(scene.Erro == ErroEnum.NoError);

            // Início teste
            updatecommand = new SceneUpdateCommand() {
                Id = 1,
                NextState = StateEnum.Pendente,
                OperationHour = DateTime.Now.AddMinutes(10)
            };

            scene.UpdateState(updatecommand);
            Assert.True(scene.Erro == ErroEnum.CantUndoOperation);
        }

        [Fact]
        [Trait("Name", "Yu yu Hakusho")]
        public void Create_In_The_Future_Op_Not_Allowed() {
            var scene = new Scene("Yu yu Hakusho");
            var saveDate = DateTime.Now;

            // Início teste
            SceneUpdateCommand updatecommand = new SceneUpdateCommand() {
                Id = 1,
                NextState = StateEnum.Pendente,
                OperationHour = saveDate.AddMinutes(1)
            };

            scene.UpdateState(updatecommand);
            Assert.True(scene.Erro == ErroEnum.FutureAlterNotAllowed);
        }

        [Fact]
        [Trait("Name", "Avangers")]
        public void Alter_Gravada_To_Pendurada_Op_Not_Allowed() {
            var scene = new Scene("Avangers");

            #region Prepara o ambiente para o teste
            var updatecommand = new SceneUpdateCommand() {
                Id = 1,
                NextState = StateEnum.Preparada,
                OperationHour = DateTime.Now
            };
            scene.UpdateState(updatecommand);

            updatecommand = new SceneUpdateCommand() {
                Id = 1,
                NextState = StateEnum.Gravada,
                OperationHour = DateTime.Now
            };
            scene.UpdateState(updatecommand);
            #endregion

            updatecommand = new SceneUpdateCommand() {
                Id = 1,
                NextState = StateEnum.Pendurada,
                OperationHour = DateTime.Now
            };
            scene.UpdateState(updatecommand);
            Assert.True(scene.Erro == ErroEnum.OpNotAllowed);
        }
    }
}
