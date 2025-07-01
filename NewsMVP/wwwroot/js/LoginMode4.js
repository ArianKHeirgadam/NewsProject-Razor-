// Mobile Navigation
function openNav() {
    document.getElementById("mySidenav").style.width = "300px";
}

function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
}

// Update current date
document.addEventListener('DOMContentLoaded', function () {
    function updateDate() {
        const dateElement = document.querySelector('.header-date');
        if (dateElement) {
            const now = new Date();
            // Format date in Persian
            const options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
            const persianDate = new Intl.DateTimeFormat('fa-IR', options).format(now);
            dateElement.textContent = persianDate;
        }
    }
    updateDate();
});

// Dynamic Scroll Buttons
document.addEventListener('DOMContentLoaded', function () {
    const btn_Dynamic = document.querySelectorAll('.btn_Dynamic');
    const mainMenu = document.querySelector('.nav_menud');
    function checkScrollButtons() {
        const listWidth = mainMenu.scrollWidth;
        const containerWidth = mainMenu.offsetWidth;

        if (listWidth <= containerWidth) {
            btn_Dynamic.forEach(btn => btn.style.display = 'none');
            mainMenu.style.margin = '15px auto 30px auto';
        } else {
            btn_Dynamic.forEach(btn => btn.style.display = 'block');
            mainMenu.style.margin = '';
        }
    }
    checkScrollButtons();
    window.addEventListener('resize', checkScrollButtons);
});

// Login Form Validation
document.addEventListener('DOMContentLoaded', function() {
    const loginForm = document.querySelector('.login-form');

    if (loginForm) {
        loginForm.addEventListener('submit', function(e) {
            e.preventDefault();

            const usernameInput = document.getElementById('username');
            const passwordInput = document.getElementById('password');

            const username = usernameInput.value.trim();
            const password = passwordInput.value.trim();

            let isValid = true;

            // Basic validation
            if (username === '') {
                alert('لطفاً نام کاربری یا ایمیل خود را وارد کنید.');
                isValid = false;
            }

            if (password === '') {
                alert('لطفاً رمز عبور خود را وارد کنید.');
                isValid = false;
            }

            if (isValid) {
                // Here you would typically send the data to your server for authentication
                console.log('Login attempt with:', { username, password });
                alert('ورود موفقیت آمیز!'); // Replace with actual login logic
                // Redirect or perform other actions after successful login
            }
        });
    }
}); 