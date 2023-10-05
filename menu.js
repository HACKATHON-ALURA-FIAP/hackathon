window.addEventListener("scroll", function () {
  let header = document.querySelector("#header");
  header.classList.toggle("rolagem", window.scrollY > 0);
});

// menu responsivo
function menuShow() {
  let menuMobile = document.querySelector(".mobile_menu");

  if (menuMobile.classList.contais("open")) {
    menuMobile.classList.remove("open");
  } else {
    menuMobile.classList.add("open");
  }
}
