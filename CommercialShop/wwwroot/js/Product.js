document.addEventListener('DOMContentLoaded', function () {
    const openBtn = document.getElementById('openSizeChart');
    const closeBtn = document.getElementById('closeSizeChart');
    const block = document.getElementById('sizeChartBlock');

  
    if (openBtn) {
        openBtn.addEventListener('click', function (e) {
            e.preventDefault();
            block.classList.remove('d-none');
            block.scrollIntoView({ behavior: 'smooth', block: 'center' });
        });
    }

    if (closeBtn) {
        closeBtn.addEventListener('click', function () {
            block.classList.add('d-none');
        });
    }

    // close on Escape
    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape' && !block.classList.contains('d-none')) {
            block.classList.add('d-none');
        }
    });
});

// pass url to cart page



