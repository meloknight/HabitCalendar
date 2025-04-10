

const scrollBtnBottom = document.getElementById('scroll-to-bottom-button');
const scrollBtnTop = document.getElementById('scroll-to-top-button');
const calendarSection = document.getElementById('calendar-section');

function scrollToBottom() {
    document.documentElement.scrollTo({
        top: document.documentElement.scrollHeight,
        behavior: 'smooth'
    });
}
scrollBtnBottom.addEventListener('click', () => scrollToBottom());


function scrollToTop() {
    document.documentElement.scrollTo({
        top: 0,
        behavior: 'smooth'
    });
}
scrollBtnTop.addEventListener('click', () => scrollToTop());