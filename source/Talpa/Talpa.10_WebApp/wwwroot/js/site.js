// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let sidenavIsVisible = true;

function handleResize() {
    if (document.body.clientWidth < 600) {
        document.getElementById("sidenavbutton").innerText = ">";
        if (sidenavIsVisible != false) {
            sidenav();
        }
        sidenavIsVisible = false;
    } else {
        document.getElementById("sidenavbutton").innerText = "<";
        if (sidenavIsVisible != true) {
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
        }, i);
    }
}

function moveSideNavButton() {
    const sidenavbutton = document.getElementById("sidenavbutton");
    if (!sidenavbutton) {
        return;
    }
    sidenavbutton.innerText = sidenavIsVisible ? ">" : "<";
    const width = 215;

    for (let i = width; i >= 0; i--) {
        let pixel = sidenavIsVisible ? i : width - i;
        setTimeout(() => {
            sidenavbutton.style.left = pixel + "px";
        }, width - i);
    }
}

function moveMain() {
    const main = document.getElementById("main");
    if (!main) {
        return;
    }

    for (let i = 300; i >= 20; i--) {
        let pixel = sidenavIsVisible ? i : 300 - i;
        setTimeout(() => {
            main.style.marginLeft = pixel + "px";
        }, 300 - i);
    }
}