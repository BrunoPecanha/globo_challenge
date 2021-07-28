import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Scene } from './model/Scene';
import { SceneService } from './service/scence.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: []
})
export class AppComponent {
  public mode = 'list';
  public title = 'Controle de Cenas';
  public scenes: Scene[] = [];
  public form!: FormGroup;
  public _service: SceneService;

  constructor(private fb: FormBuilder, service: SceneService) {   
    this.form = this.fb.group({
      title: ['', Validators.compose([
        Validators.minLength(3),
        Validators.maxLength(14),
        Validators.required
      ])]
    });
    this._service = service;
    this.carregar();
  }

  incluir() {
    var name = this.form.controls['title'].value;
    if (this.scenes.length < 1)    {
     this.scenes.push(new Scene(0, 1, name, new Date()))
      this.salvar(name);
      this.cleanData();
    }     
    else
    {
      const index = Math.max.apply(null, this.scenes.map(x => x.id)) + 1;
      this.scenes.push(new Scene(0, 1, name, new Date()))
      var newScene = this.salvar(name);
      this.scenes.push(newScene);
      
      this.cleanData();
    }
  }

  cleanData() {
    this.form.reset();
  }  

   salvar(name: string) : Scene {
    const dado = JSON.stringify(this.scenes);
    localStorage.setItem('scenes', dado);
    this.mode = 'list';

    return new Scene(1, 1, "", new Date());
  }

  carregar(){    
    // Popular a lista 
    const dado = this._service.getScenes(0, 100);
  }

  changeMode(mode: string){
    this.mode = mode;
  }
}
