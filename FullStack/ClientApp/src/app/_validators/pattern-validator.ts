import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms'

export function PatternValidator(regex: RegExp, error: ValidationErrors): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } => {
    if (!control.value) {
        // if control is empty return no error
        return null;
      }

    //   if (/\d/.test(control.value))
    //   {
    //       return { hasCapitalCase: true };
    //   }
    
///\d/, { hasNumber: true }
///[A-Z]/, { hasCapitalCase: true }
///[a-z]/, { hasSmallCase: true }
///[ [!@#$%^&*()_+-=[]{};':"|,.<>/?]/](<mailto:!@#$%^&*()_+-=[]{};':"|,.<>/?]/>), { hasSpecialCharacters: true }

     // test the value of the control against the regexp supplied
     const valid = regex.test(control.value);

     // if true, return no error (no error), else return error passed in the second parameter
     return valid ? null : error;
   };
}