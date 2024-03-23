document.addEventListener("DOMContentLoaded", function () {
    var currentUrl = window.location.pathname;
    if ((currentUrl.match(/\//g) || []).length < 2) {
        var activeLink = document.querySelector('.submenu a[href="' + currentUrl + '"]');
        if (activeLink) {
            var parent = activeLink.parentElement;
            while (parent && parent !== document.body) {
                if (parent.classList.contains('submenu')) {
                    parent.parentElement.querySelector('a.dropdown-toggle').classList.add('active');
                    parent.style.color = '#669fc7'
                    parent.style.display = 'block';
                }
                parent = parent.parentElement;
            }
            activeLink.classList.add('active');
            activeLink.style.color = '#669fc7'
        }
    } else {
        var activeLink = document.querySelector('.submenu a[href="' + currentUrl.match(/^\/[^\/]+/)[0] + '"]');
        if (activeLink) {
            var parent = activeLink.parentElement;
            while (parent && parent !== document.body) {
                if (parent.classList.contains('submenu')) {
                    parent.parentElement.querySelector('a.dropdown-toggle').classList.add('active');
                    parent.style.color = '#669fc7'
                    parent.style.display = 'block';
                }
                parent = parent.parentElement;
            }
            activeLink.classList.add('active');
            activeLink.style.color = '#669fc7'
        }
    }

});