// Mobile Navigation
function openNav() {
    document.getElementById("mySidenav").style.width = "300px";
}

function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
}

// FAQ Accordion
document.addEventListener('DOMContentLoaded', function() {
    const faqItems = document.querySelectorAll('.faq-item');
    
    faqItems.forEach(item => {
        const question = item.querySelector('.faq-question');
        
        question.addEventListener('click', () => {
            // Close all other items
            faqItems.forEach(otherItem => {
                if (otherItem !== item) {
                    otherItem.classList.remove('active');
                }
            });
            
            // Toggle current item
            item.classList.toggle('active');
        });
    });
});

// Form Validation and Submission
document.addEventListener('DOMContentLoaded', function() {
    const contactForm = document.querySelector('.contact-form');
    
    if (contactForm) {
        contactForm.addEventListener('submit', function(e) {
            e.preventDefault();
            
            // Get form data
            const formData = {
                name: document.getElementById('name').value,
                email: document.getElementById('email').value,
                subject: document.getElementById('subject').value,
                message: document.getElementById('message').value
            };
            
            // Validate form
            if (validateForm(formData)) {
                // Here you would typically send the data to your server
                console.log('Form data:', formData);
                
                // Show success message
                showAlert('پیام شما با موفقیت ارسال شد. به زودی با شما تماس خواهیم گرفت.', 'success');
                
                // Reset form
                contactForm.reset();
            }
        });
    }
});

// Form validation function
function validateForm(data) {
    let isValid = true;
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    
    // Validate name
    if (!data.name.trim()) {
        showAlert('لطفاً نام خود را وارد کنید.', 'error');
        isValid = false;
    }
    
    // Validate email
    if (!data.email.trim() || !emailRegex.test(data.email)) {
        showAlert('لطفاً یک ایمیل معتبر وارد کنید.', 'error');
        isValid = false;
    }
    
    // Validate subject
    if (!data.subject.trim()) {
        showAlert('لطفاً موضوع پیام را وارد کنید.', 'error');
        isValid = false;
    }
    
    // Validate message
    if (!data.message.trim()) {
        showAlert('لطفاً پیام خود را وارد کنید.', 'error');
        isValid = false;
    }
    
    return isValid;
}

// Alert message function
function showAlert(message, type) {
    // Remove any existing alerts
    const existingAlert = document.querySelector('.alert');
    if (existingAlert) {
        existingAlert.remove();
    }
    
    // Create alert element
    const alert = document.createElement('div');
    alert.className = `alert alert-${type}`;
    alert.textContent = message;
    
    // Insert alert before the form
    const form = document.querySelector('.contact-form');
    form.parentNode.insertBefore(alert, form);
    
    // Remove alert after 5 seconds
    setTimeout(() => {
        alert.remove();
    }, 5000);
}

// Smooth scroll for navigation
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault();
        
        const target = document.querySelector(this.getAttribute('href'));
        if (target) {
            target.scrollIntoView({
                behavior: 'smooth',
                block: 'start'
            });
        }
    });
});

// Add active class to current navigation item
function setActiveNavItem() {
    const currentPath = window.location.pathname;
    const navLinks = document.querySelectorAll('.nav-link');
    
    navLinks.forEach(link => {
        if (link.getAttribute('href') === currentPath) {
            link.classList.add('active');
        }
    });
}

// Call setActiveNavItem when DOM is loaded
document.addEventListener('DOMContentLoaded', setActiveNavItem); 