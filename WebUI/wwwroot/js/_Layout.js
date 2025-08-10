const openMenu = document.getElementById("open-menu");
const closeMenu = document.getElementById("close-menu");
const menu = document.getElementById("menu");
const languageName = document.getElementById("language-name");
const choseLanguageMenu = document.getElementById("chose-language");
const languageInputs = document.querySelectorAll('input[name="language"]');
const cookieLanguageValue = getCookieValue(".AspNetCore.Culture");

if (cookieLanguageValue !== null &&  cookieLanguageValue.includes("uk-UA")) {
    document.querySelector('input[name="language"][value="uk-UA"]').checked = true;
    languageName.innerHTML ="UA&#9662;"
}
else {
    document.querySelector('input[name="language"][value="en-GB"]').checked = true;
    languageName.innerHTML = "EN&#9662;"
}

openMenu.addEventListener("click", () => {
    openMenu.classList.add("clicked");
    closeMenu.classList.remove("clicked");
    menu.classList.add("opened");
});

closeMenu.addEventListener("click", () => {
    openMenu.classList.remove("clicked");
    closeMenu.classList.add("clicked");
    menu.classList.remove("opened");
});

//Language
languageName.addEventListener("click", () => {
    choseLanguageMenu.classList.toggle("opened");
});

document.body.addEventListener("click", (event) => {
    if (!event.target.closest("#language")) {
        choseLanguageMenu.classList.remove("opened");
    }
});

languageInputs.forEach(languageInput => {
    languageInput.addEventListener("change", () => {
        document.querySelector('input[name="culture"]').value = document.querySelector('input[name="language"]:checked').value;
        choseLanguageMenu.submit();
    });
});

function getCookieValue(name) {
    const cookies = document.cookie.split('; ');
    for (let cookie of cookies) {
        const [key, value] = cookie.split('=');
        if (key === name) {
            return value; 
        }
    }
    return null; 
}