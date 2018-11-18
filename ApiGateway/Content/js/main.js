var logoutBtn = document.getElementById('logout');

if (logoutBtn !== null) {
    logoutBtn.addEventListener('click', function () {
        document.cookie = "token=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    });
}