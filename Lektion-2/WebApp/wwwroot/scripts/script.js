let scrollPosition = window.screenY

const menuSection = document.getElementById("menu-section")
const addClassOnScroll = () =>  menuSection.classList.add("bg-white")
const removeClassOnScroll = () =>  menuSection.classList.remove("bg-white")

window.addEventListener('scroll', () => {
    scrollPosition = window.scrollY

    if (scrollPosition >= 1)
        addClassOnScroll()
    else
        removeClassOnScroll()
})