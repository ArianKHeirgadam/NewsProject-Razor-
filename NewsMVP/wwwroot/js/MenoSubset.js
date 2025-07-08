document.addEventListener('DOMContentLoaded', function () {
    const menuItems = document.querySelectorAll('.has-submenu');

    menuItems.forEach(item => {
        item.addEventListener('click', (event) => {
            // اگه روی لینک کلیک شده، بذار بره صفحه خودش
            if (event.target.tagName.toLowerCase() === 'a') {
                return;
            }

            event.stopPropagation();

            const submenu = item.querySelector('.submenu');
            const submenuOrginal = item.querySelector('.submenuOrginal');

            if (item.classList.contains('active')) {
                item.classList.remove('active');
                if (submenu) submenu.style.display = 'none';
                if (submenuOrginal) submenuOrginal.style.display = 'none';
            } else {
                document.querySelectorAll('.has-submenu').forEach(menuItem => {
                    menuItem.classList.remove('active');
                    const sm = menuItem.querySelector('.submenu');
                    const smo = menuItem.querySelector('.submenuOrginal');
                    if (sm) sm.style.display = 'none';
                    if (smo) smo.style.display = 'none';
                });

                item.classList.add('active');
                if (submenu) submenu.style.display = 'grid';
                if (submenuOrginal) submenuOrginal.style.display = 'flex';
            }
        });
    });

    // کلیک خارج از منوها → همه چی ببند
    document.addEventListener('click', () => {
        document.querySelectorAll('.has-submenu').forEach(item => {
            item.classList.remove('active');
            const sm = item.querySelector('.submenu');
            const smo = item.querySelector('.submenuOrginal');
            if (sm) sm.style.display = 'none';
            if (smo) smo.style.display = 'none';
        });
    });

    // نمایش/مخفی کردن منو با حرکت موس (اختیاری)
    menuItems.forEach(item => {
        item.addEventListener('mouseenter', () => {
            item.querySelector('.submenu')?.style.setProperty('display', 'grid');
            item.querySelector('.submenuOrginal')?.style.setProperty('display', 'flex');
        });

        item.addEventListener('mouseleave', () => {
            if (!item.classList.contains('active')) {
                item.querySelector('.submenu')?.style.setProperty('display', 'none');
                item.querySelector('.submenuOrginal')?.style.setProperty('display', 'none');
            }
        });
    });
});
