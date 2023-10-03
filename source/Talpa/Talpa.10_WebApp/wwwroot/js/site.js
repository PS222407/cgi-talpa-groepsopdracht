// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const mobileBreakPoint = 600;
let sidenavIsVisible = true;
let initializedNavMovements = false;

setTimeout(() => {
    initializedNavMovements = true;
}, 750);

function handleResize() {
    if (document.body.clientWidth < mobileBreakPoint) {
        document.getElementById("sidenavbutton").innerText = ">";
        if (sidenavIsVisible !== false) {
            sidenav();
        }
        sidenavIsVisible = false;
    } else {
        document.getElementById("sidenavbutton").innerText = "<";
        if (sidenavIsVisible !== true) {
            sidenav();
        }
        sidenavIsVisible = true;
    }
}

window.addEventListener('resize', handleResize);
handleResize();

function sidenav() {
    moveSideNav();
    moveSideNavButton();
    moveMain();

    initializedNavMovements = true;
    sidenavIsVisible = !sidenavIsVisible;
}

function moveSideNav() {
    const sidenav = document.getElementById("sidenav");
    if (!sidenav) {
        return;
    }
    const width = 260;
    for (let i = 0; i < width; i++) {
        let pixel = sidenavIsVisible ? i * -1 : i - width;
        setTimeout(() => {
            sidenav.style.left = pixel + "px";
        }, initializedNavMovements ? i : 0);
    }
}

function moveSideNavButton() {
    const sidenavbutton = document.getElementById("sidenavbutton");
    if (!sidenavbutton) {
        return;
    }
    sidenavbutton.innerText = sidenavIsVisible ? ">" : "<";
    const width = 182;

    for (let i = width; i >= 0; i--) {
        let pixel = sidenavIsVisible ? i : width - i;
        setTimeout(() => {
            sidenavbutton.style.left = pixel + "px";
        }, initializedNavMovements ? width - i : 0);
    }
}

function moveMain() {
    const main = document.getElementById("main");
    if (!main) {
        return;
    }
    const margin = 300;

    for (let i = margin; i >= 20; i--) {
        let pixel = sidenavIsVisible ? i : margin - i;
        setTimeout(() => {
            main.style.marginLeft = pixel + "px";
        }, initializedNavMovements ? 300 - i : 0);
    }
    
    if (document.body.clientWidth < mobileBreakPoint) {
        if (sidenavIsVisible) {
            document.body.style.overflow = 'auto';
        } else {
            document.body.style.overflow = 'hidden';
        }
    }
}