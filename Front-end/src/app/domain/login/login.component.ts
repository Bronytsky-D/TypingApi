import { Component, OnInit, inject } from '@angular/core';
import { AuthService } from '../../core/modules/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  email: string = '';
  password: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void { }

  login(): void {
    this.authService.login({ email: this.email, password: this.password }).subscribe(
      (response) => {
        if (response.isSuccess) {
          this.router.navigate(['/typing-game']);
        } else {
          alert('Login failed: ' + response.message);
        }
      },
      (error) => {
        console.error('Login error', error);
        alert('An error occurred during login.');
      }
    );
  }
}