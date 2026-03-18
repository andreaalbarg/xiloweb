/* =========================================
   XILO VENTURE STUDIO — Blazor JS Interop
   ========================================= */

// Smooth scroll to element by id
window.scrollToElement = function (id) {
    const el = document.getElementById(id);
    if (el) {
        el.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
};

// Smooth scroll to top
window.scrollToTop = function () {
    window.scrollTo({ top: 0, behavior: 'smooth' });
};

// Get current scroll position
window.getScrollPosition = function () {
    return window.scrollY;
};

// Initialize navbar scroll effect
window.initNavbarScroll = function (dotNetHelper) {
    window.addEventListener('scroll', function () {
        const scrolled = window.scrollY > 60;
        if (dotNetHelper) {
            dotNetHelper.invokeMethodAsync('OnScrolled', scrolled);
        }
        const navbar = document.querySelector('.navbar');
        if (navbar) {
            if (scrolled) {
                navbar.classList.add('scrolled');
            } else {
                navbar.classList.remove('scrolled');
            }
        }
    });
};

// Initialize fade-in on scroll (Intersection Observer)
window.initFadeIn = function () {
    const selectors = [
        '.hero-content',
        '.statement-large',
        '.company-item',
        '.demoday-top',
        '.demoday-gallery',
        '.podcast-top',
        '.podcast-gallery-wrapper',
        '.partner-container',
        '.spotlight-heading',
        '.spotlight-inner',
        '.community-container'
    ];

    const faders = document.querySelectorAll(selectors.join(', '));

    const appearOptions = {
        threshold: 0.1,
        rootMargin: '0px 0px -40px 0px'
    };

    const appearOnScroll = new IntersectionObserver(function (entries, observer) {
        entries.forEach(function (entry) {
            if (entry.isIntersecting) {
                entry.target.classList.add('appear');
                observer.unobserve(entry.target);
            }
        });
    }, appearOptions);

    faders.forEach(function (fader) {
        fader.classList.add('fade-in');
        appearOnScroll.observe(fader);
    });
};

// Initialize podcast slider
window.initPodcastSlider = function (dotNetHelper, totalItems) {
    let currentPage = 0;
    const itemsPerPage = 3;
    const totalPages = Math.ceil(totalItems / itemsPerPage);

    window._podcastSlider = {
        currentPage: 0,
        totalPages: totalPages,
        updateSlider: function () {
            const gallery = document.querySelector('.podcast-gallery');
            if (!gallery) return;
            const firstItem = gallery.children[0];
            if (!firstItem) return;
            const gap = parseFloat(getComputedStyle(gallery).gap) || 0;
            const itemWidth = firstItem.offsetWidth + gap;
            const scrollAmount = window._podcastSlider.currentPage * itemsPerPage * itemWidth;
            gallery.style.transform = 'translateX(-' + scrollAmount + 'px)';
        }
    };

    window.addEventListener('resize', function () {
        if (window._podcastSlider) window._podcastSlider.updateSlider();
    });

    window._podcastSlider.updateSlider();
    return { currentPage: 0, totalPages: totalPages };
};

window.podcastPrev = function () {
    if (window._podcastSlider && window._podcastSlider.currentPage > 0) {
        window._podcastSlider.currentPage--;
        window._podcastSlider.updateSlider();
        return window._podcastSlider.currentPage;
    }
    return window._podcastSlider ? window._podcastSlider.currentPage : 0;
};

window.podcastNext = function () {
    if (window._podcastSlider && window._podcastSlider.currentPage < window._podcastSlider.totalPages - 1) {
        window._podcastSlider.currentPage++;
        window._podcastSlider.updateSlider();
        return window._podcastSlider.currentPage;
    }
    return window._podcastSlider ? window._podcastSlider.currentPage : 0;
};

window.getPodcastPage = function () {
    return window._podcastSlider ? window._podcastSlider.currentPage : 0;
};

window.getPodcastTotalPages = function () {
    return window._podcastSlider ? window._podcastSlider.totalPages : 1;
};

// Close mobile nav when clicking outside
window.initMobileNavClose = function (dotNetHelper) {
    document.addEventListener('click', function (e) {
        const toggle = document.querySelector('.nav-toggle');
        const navLinks = document.querySelector('.nav-links');
        if (toggle && navLinks && navLinks.classList.contains('active')) {
            if (!toggle.contains(e.target) && !navLinks.contains(e.target)) {
                dotNetHelper.invokeMethodAsync('CloseNav');
            }
        }
    });
};

// Set document language attribute
window.setHtmlLang = function (lang) {
    document.documentElement.lang = lang;
};
