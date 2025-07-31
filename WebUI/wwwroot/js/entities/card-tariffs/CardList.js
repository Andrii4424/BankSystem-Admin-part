import { GeneralListMethods } from "../../GeneralListMethods.js";


CheckAndChangeTextColor() 

GeneralListMethods.EmptyListTitleChecker("Card");


function CheckAndChangeTextColor() {
    document.querySelectorAll(".element-block").forEach(el => {
        const style = window.getComputedStyle(el);
        const bg = style.backgroundColor;

        const rgbMatch = bg.match(/\d+/g);
        if (!rgbMatch || rgbMatch.length < 3) return;

        const r = parseInt(rgbMatch[0]).toString(16).padStart(2, '0');
        const g = parseInt(rgbMatch[1]).toString(16).padStart(2, '0');
        const b = parseInt(rgbMatch[2]).toString(16).padStart(2, '0');
        const hex = `${r}${g}${b}`;

        if (isDarkColor(hex)) {
            el.style.color = "white";
        } else {
            el.style.color = "black";
        }
    });
}

//For changing text color when background is dark
function isDarkColor(hexColor) {
    if (!hexColor) return false;

    hexColor = hexColor.replace("#", "");
    if (hexColor.length === 3) {
        hexColor = hexColor.split("").map(c => c + c).join("");
    }

    if (hexColor.length !== 6) return false;

    const r = parseInt(hexColor.substring(0, 2), 16);
    const g = parseInt(hexColor.substring(2, 4), 16);
    const b = parseInt(hexColor.substring(4, 6), 16);

    const brightness = 0.299 * r + 0.587 * g + 0.114 * b;

    return brightness < 128;
}
