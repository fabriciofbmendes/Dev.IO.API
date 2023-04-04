import { Component, OnInit } from '@angular/core';
import { UserService } from '../userService';

@Component({
  selector: 'app-menu-user',
  templateUrl: './menu.user.component.html'
})
export class MenuUserComponent {

  saudacao: string;

  constructor(private userService: UserService) {  }

  logout() {
    this.userService.logout();
    return true;
  }

  userLogado(): boolean {
    var user = this.userService.obterUsuario();
    if (user) {
      this.saudacao = "Olá " + user.email;
      return true;
    }

    return false;
  }
}
