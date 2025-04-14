

const scrollBtnBottom = document.getElementById('scroll-to-bottom-button');
const scrollBtnTop = document.getElementById('scroll-to-top-button');
const calendarSection = document.getElementById('calendar-section');

window.addEventListener('DOMContentLoaded', () => {
    console.log(document.body.scrollHeight);

    if (document.body.scrollHeight > 1500) {
        scrollBtnBottom.classList.remove('scroll-button-not-visible')
        scrollBtnTop.classList.remove('scroll-button-not-visible')
    } else {
        scrollBtnBottom.classList.add('scroll-button-not-visible')
        scrollBtnTop.classList.add('scroll-button-not-visible')
    }
})

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
