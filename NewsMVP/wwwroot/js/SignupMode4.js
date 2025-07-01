document.addEventListener('DOMContentLoaded', function() {
    const signupForm = document.querySelector('.signup-form');
    const passwordInput = document.getElementById('password');
    const strengthMeter = document.querySelector('.password-strength');

    // Password strength checker
    passwordInput.addEventListener('input', function() {
        const password = this.value;
        let strength = 0;

        // Length check
        if (password.length >= 8) strength++;

        // Contains number
        if (/\d/.test(password)) strength++;

        // Contains lowercase
        if (/[a-z]/.test(password)) strength++;

        // Contains uppercase
        if (/[A-Z]/.test(password)) strength++;

        // Contains special character
        if (/[^A-Za-z0-9]/.test(password)) strength++;

        // Update strength meter
        strengthMeter.className = 'password-strength';
        const strengthText = strengthMeter.querySelector('.strength-text');

        if (password.length === 0) {
            strengthMeter.classList.add('strength-weak');
            strengthText.textContent = 'قدرت رمز عبور: ضعیف';
        } else if (strength === 1) {
            strengthMeter.classList.add('strength-weak');
            strengthText.textContent = 'قدرت رمز عبور: ضعیف';
        } else if (strength === 2) {
            strengthMeter.classList.add('strength-medium');
            strengthText.textContent = 'قدرت رمز عبور: متوسط';
        } else if (strength === 3) {
            strengthMeter.classList.add('strength-strong');
            strengthText.textContent = 'قدرت رمز عبور: قوی';
        } else if (strength >= 4) {
            strengthMeter.classList.add('strength-very-strong');
            strengthText.textContent = 'قدرت رمز عبور: بسیار قوی';
        }
    });

    // Form validation
    signupForm.addEventListener('submit', function(e) {
        e.preventDefault();

        const fullname = document.getElementById('fullname').value.trim();
        const tel = document.getElementById('tel').value.trim();
        const password = passwordInput.value.trim();

        let isValid = true;
        let errorMessage = '';

        // Validate full name
        if (fullname === '') {
            errorMessage = 'لطفاً نام و نام خانوادگی خود را وارد کنید.';
            isValid = false;
        } else if (fullname.length < 2) {
            errorMessage = 'نام و نام خانوادگی باید حداقل 2 کاراکتر باشد.';
            isValid = false;
        }

        // Validate telephone number
        if (tel === '') {
            errorMessage = 'لطفاً شماره تماس خود را وارد کنید.';
            isValid = false;
        } else if (!/^09[0-9]{9}$/.test(tel)) {
            errorMessage = 'شماره تماس باید با 09 شروع شود و 11 رقم باشد.';
            isValid = false;
        }

        // Validate password
        if (password === '') {
            errorMessage = 'لطفاً رمز عبور را وارد کنید.';
            isValid = false;
        } else if (password.length < 8) {
            errorMessage = 'رمز عبور باید حداقل 8 کاراکتر باشد.';
            isValid = false;
        }

        if (!isValid) {
            alert(errorMessage);
            return;
        }

        // If validation passes, you can submit the form
        // Here you would typically send the data to your server
        console.log('Signup attempt with:', { fullname, tel, password });
        alert('ثبت نام با موفقیت انجام شد!');
        // Redirect to login page or dashboard
        window.location.href = 'LoginMode4.html';
    });
}); 