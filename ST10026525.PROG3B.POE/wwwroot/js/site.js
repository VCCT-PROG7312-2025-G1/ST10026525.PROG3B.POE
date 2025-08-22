// Write your JavaScript code.
(function () {
    var navbar = document.querySelector('.navbar');
    if (!navbar) return;

    var lastKnownScrollY = 0;
    var ticking = false;

    function onScroll() {
        lastKnownScrollY = window.scrollY || window.pageYOffset;
        if (!ticking) {
            window.requestAnimationFrame(function () {
                navbar.classList.toggle('scrolled', lastKnownScrollY > 10);
                ticking = false;
            });
            ticking = true;
        }
    }

    window.addEventListener('scroll', onScroll, { passive: true });
    // Initialize state
    onScroll();
})();
