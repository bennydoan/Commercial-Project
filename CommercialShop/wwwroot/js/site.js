document.addEventListener('DOMContentLoaded', function () {
    try {
        // rotating messages (existing)
        const message1 = document.getElementById('msg-returns');
        const message2 = document.getElementById('msg-delivery');
        if (message1 && message2) {
            const intervalDuration = 5000;
            function rotateMessages() {
                const isMsg1Visible = !message1.classList.contains('d-none');
                if (isMsg1Visible) {
                    message1.classList.add('d-none');
                    message2.classList.remove('d-none');
                } else {
                    message2.classList.add('d-none');
                    message1.classList.remove('d-none');
                }
            }
            rotateMessages();
            setInterval(rotateMessages, intervalDuration);
        }

        // search toggle
        const container = document.getElementById('searchBoxContainer');
        const icon = document.querySelector('#searchBoxContainer .search-icon') || document.querySelector('.search-icon');
        const input = document.getElementById('searchBox');

        if (!container) console.warn('searchBoxContainer not found');
        if (!icon) console.warn('search icon not found');
        if (!input) console.warn('search input not found');

        function isSmall() {
            return window.innerWidth < 768;
        }

        if (icon && input) {
            // initial state
            if (isSmall()) input.classList.add('d-none'); else input.classList.remove('d-none');

            icon.addEventListener('click', function (e) {
                if (!isSmall()) {
                    input.focus();
                    return;
                }
                if (input.classList.contains('d-none')) {
                    input.classList.remove('d-none');
                    input.classList.add('mobile-visible', 'd-block');
                    setTimeout(() => input.focus(), 40);
                } else {
                    input.classList.remove('mobile-visible', 'd-block');
                    input.classList.add('d-none');
                }
            });

            // hide when clicking outside (mobile)
            document.addEventListener('click', function (ev) {
                if (!isSmall()) return;
                if (!input.classList.contains('mobile-visible')) return;
                if (!container.contains(ev.target)) {
                    input.classList.remove('mobile-visible', 'd-block');
                    input.classList.add('d-none');
                }
            });

            // hide on Escape
            document.addEventListener('keydown', function (ev) {
                if (ev.key === 'Escape' && input.classList.contains('mobile-visible')) {
                    input.classList.remove('mobile-visible', 'd-block');
                    input.classList.add('d-none');
                }
            });

            window.addEventListener('resize', function () {
                if (isSmall()) {
                    if (!input.classList.contains('mobile-visible')) {
                        input.classList.add('d-none');
                        input.classList.remove('d-block');
                    }
                } else {
                    input.classList.remove('d-none', 'mobile-visible');
                    input.classList.add('d-md-inline-block');
                }
            });
        }
    } catch (err) {
        console.error('site.js error', err);
    }
});



const shirts = document.querySelectorAll(".chosenShirt");

shirts.forEach(shirt => {
    shirt.addEventListener("click", () => {
        // remove border from all
        shirts.forEach(s => s.classList.remove('active'));

        // add border to clicked image
        shirt.classList.add('active');
    });
});


const chosenGender = document.querySelectorAll(".genderBtn");

chosenGender.forEach(c => {
    c.addEventListener("click", () => {
        chosenGender.forEach(cg => cg.classList.remove('active'));
        c.classList.add('active');
    });
});

const kitBtn = document.querySelectorAll(".secondBtn");

kitBtn.forEach(k => {
    k.addEventListener("click", () => {
        kitBtn.forEach(kb => kb.classList.remove("active"));
        k.classList.add("active");
    });
});


const sleeveLength = document.querySelectorAll(".thirdBtn");

sleeveLength.forEach(s => {
    s.addEventListener("click", () => {
        sleeveLength.forEach(sl => sl.classList.remove('active'));
        s.classList.add('active');
    });
});

const size = document.querySelectorAll(".fourthBtn");

size.forEach(s => {
    s.addEventListener("click", () => {
        size.forEach(sl => sl.classList.remove('active'));
        s.classList.add('active');
    });
});