const scrollBtnBottom = document.getElementById('scroll-to-bottom-button');
const scrollBtnTop = document.getElementById('scroll-to-top-button');
const calendarSection = document.getElementById('calendar-section');
const calendarDayModifyCheckboxes = document.getElementsByClassName('form-container--single-day__checkbox');
const calendarDayForm = document.getElementById('form-container');

//const len = calendarDayModifyCheckboxes.length;
//if (len > 0) {
//    for (let i = 0; i < len; i++) {
//        console.log(calendarDayModifyCheckboxes[i])
//    }
//}
//console.log(calendarDayForm);

window.addEventListener('DOMContentLoaded', () => {
    if (scrollBtnBottom != null && scrollBtnTop != null) {
    // Add or remove the scrollTo button
        if (document.body.scrollHeight > 1500) {
            scrollBtnBottom.classList.remove('scroll-button-not-visible')
            scrollBtnTop.classList.remove('scroll-button-not-visible')
        } else {
            scrollBtnBottom.classList.add('scroll-button-not-visible')
            scrollBtnTop.classList.add('scroll-button-not-visible')
        }

    }

    if (calendarDayForm) {
        //Remove default form behaviour for CalendarDayModify form to stop submit on Enter.
        calendarDayForm.addEventListener('keydown', function (e) {
            if (e.key === 'Enter') {
                if (e.target.id === 'form-container--single-day__checkbox') {
                    e.preventDefault();

                    const len = calendarDayModifyCheckboxes.length;
                    for (let i = 0; i < len; i++) {
                        if (e.target.name === `[${i}].isHabitCompleted`) {
                            calendarDayModifyCheckboxes[i].checked = !calendarDayModifyCheckboxes[i].checked;
                        }
                    }
                }
                }
            })

    }
})

function scrollToBottom() {
    document.documentElement.scrollTo({
        top: document.documentElement.scrollHeight,
        behavior: 'smooth'
    });
}
if (scrollBtnBottom != null) {
    scrollBtnBottom.addEventListener('click', () => scrollToBottom());
}


function scrollToTop() {
    document.documentElement.scrollTo({
        top: 0,
        behavior: 'smooth'
    });
}
if (scrollBtnTop != null) {
    scrollBtnTop.addEventListener('click', () => scrollToTop());
}
