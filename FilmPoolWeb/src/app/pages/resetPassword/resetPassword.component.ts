import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { ResetPasswordModel } from 'src/app/models/resetPassword.model';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-reset-password',
  templateUrl: './resetPassword.component.html',
  styleUrls: ['./resetPassword.component.css']
})
export class ResetPasswordComponent implements OnInit {
  resetPasswordForm!: FormGroup;
  showSuccess!: boolean;
  showError!: boolean;
  errorMessage!: string;

  private token!: string;
  private email!: string;
  
  constructor(private authService: AuthenticationService,
    private router: Router,
    private route: ActivatedRoute) { }
  
    ngOnInit(): void {
      this.resetPasswordForm = new FormGroup({
        password: new FormControl('', [Validators.required]),
        confirm: new FormControl('')
    });
    
      this.token = this.route.snapshot.queryParams['token'];
      this.email = this.route.snapshot.queryParams['email'];
    }

    public validateControl = (controlName: string) => {
        return this.resetPasswordForm.get(controlName)?.invalid && this.resetPasswordForm.get(controlName)?.touched
      }
    public hasError = (controlName: string, errorName: string) => {
        return this.resetPasswordForm.get(controlName)?.hasError(errorName)
      }

      toLogin=()=>{
        this.router.navigate(['/login']); 
      }
    public resetPassword = (resetPasswordFormValue: any) => {
        this.showError = this.showSuccess = false;
        const resetPass = { ... resetPasswordFormValue };
        const resetPassDto: ResetPasswordModel = {
          password: resetPass.password,
          confirmPassword: resetPass.confirm,
          token: this.token,
          email: this.email
        }
        this.authService.resetPassword(resetPassDto)
        .subscribe({
          next:(_) => this.showSuccess = true,
        error: (err: HttpErrorResponse) => {
          this.showError = true;
          this.errorMessage = err.message;
        }})
    }
}
  