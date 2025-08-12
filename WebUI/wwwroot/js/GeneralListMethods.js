export class GeneralListMethods {
    //General values
    static emptyListTitle = document.getElementById("empty-list-title");
    static radioInputs = document.querySelectorAll('input[name="sort"]');
    static elementsListBlock = document.getElementById("elements-block");
    static filterList = document.getElementById("filter-list");
    static searchInput = document.getElementById("search-input");
    static oneColumn = document.getElementById("one-column");
    static twoColumns = document.getElementById("two-columns");
    static deleteWindow = document.getElementById("confirm-delete-window");
    static deleteConfirmText = document.getElementById("confirm-delete-text");
    static lastSearch = (this.searchInput !== null && this.searchInput !== undefined) ? this.searchInput.value: null;

    //Checkers
    //Checks whether to display a message about an empty list
    static EmptyListTitleChecker(propertyName, uaPropertyName) {
        let cookieLanguageValue = this.getCookieValue(".AspNetCore.Culture");
        propertyName = propertyName.toLowerCase()
        if (this.GetElementsCount() === 0 && this.CheckForFilters()) {
            this.emptyListTitle.style.display = "block"
            if (cookieLanguageValue !== null && cookieLanguageValue.includes("uk-UA")) {
                this.emptyListTitle.textContent = `${uaPropertyName} не знайдені за Вашим запитом. Ви можете створити ${uaPropertyName.toLowerCase()}, натиснувши відповідну кнопку знизу`;
            }
            else {
                this.emptyListTitle.textContent = `No ${propertyName}s matched your search. You can create a new ${propertyName} by clicking the button below`;
            }
        }
        else if (this.GetElementsCount() === 0 && !this.CheckForFilters()) {
            this.emptyListTitle.style.display = "block"
            const titlePropertyName = propertyName[0].toUpperCase() + propertyName.slice(1);
            if (cookieLanguageValue !== null && cookieLanguageValue.includes("uk-UA")) {
                this.emptyListTitle.textContent = `Список ${uaPropertyName.toLowerCase()} пустий, Вы можете створити ${uaPropertyName.toLowerCase() }, натиснувши відповідну кнопку знизу`;
            }
            else {
                this.emptyListTitle.textContent = `${titlePropertyName} list is empty, you can create ${propertyName} by button bellow`;
            }
        }
        else {
            this.emptyListTitle.style.display = "none"
        }
    }
    static checkAndApplyColumns() {
        if (this.GetElementsCount() >= 2) {
            this.oneColumn.style.display = "block";
            this.twoColumns.style.display = "block";
            this.CheckLastColumn();
        }
        else {
            this.oneColumn.style.display = "none";
            this.twoColumns.style.display = "none";
            this.elementsListBlock.classList.remove("two-columns");
            this.oneColumn.classList.add("chosen");
            this.twoColumns.classList.remove("chosen");
        }
    }

    static CheckLastColumn() {
        if (this.GetElementsCount() % 2 === 1 && this.twoColumns.classList.contains("chosen")) {
            document.querySelectorAll(".element-block")[this.GetElementsCount() - 1].classList.add("last-column-element");
        }
        else {
            document.querySelectorAll(".element-block").forEach(block => {
                block.classList.remove("last-column-element");
            });
        }
    }

    //Returns true if chosen some filter otherwise false 
    static CheckForFilters() {
        const filters = this.filterList.querySelectorAll('input[type="checkbox"][name="filter"]')
        const filtersClass = document.querySelectorAll(".filter");
        if (Array.from(filters).some(filter => filter.checked)) return true;
        if (Array.from(filtersClass).some(filter => filter.checked)) return true;
        if (this.searchInput.value !== "" && this.searchInput.value !== null) return true;
        return false;
    }

    //Getters
    static GetElementsCount() {
        return  document.querySelectorAll(".element-table").length;
    }

    static GetElementsAddCount() {
        return (document.querySelectorAll(".element-table").length) > 6 ? document.querySelectorAll(".element-table").length : 6;
    }

    static GetSortUrl() {
        return document.querySelector('input[name="sort"]:checked').value;
    }
    static GetSearchUrl() {
        this.lastSearch = (this.searchInput.value !== "" && this.searchInput.value !== null) ? this.searchInput.value : "0"
        return this.lastSearch;
    }

    static GetLastSearch() {
        return this.lastSearch;
    }

    static ToBoolean(value) {
        if (value === "true" || value === "True" || value === "TRUE") {
            return true;
        }
        else if (parseInt(value) > 0) {
            return true;
        }
        else {
            return false;
        }
    }

    //Action methods
    static async AddElementsToEnd(response) {
        this.elementsListBlock.insertAdjacentHTML("beforeend", await response.text());
    }

    static OpenConfirmDeleteWindow(deleteName, uaDeleteName) {
        this.deleteWindow.style.display = "flex";
        let cookieLanguageValue = this.getCookieValue(".AspNetCore.Culture");
        if (cookieLanguageValue !== null && cookieLanguageValue.includes("uk-UA")) {
            this.deleteConfirmText.innerText = `Ви впевненні, що хочете видалити ${uaDeleteName}? Цю дію буде неможливо скасувати`;
        }
        else {
            this.deleteConfirmText.innerText = `Are you sure you want to delete ${deleteName}? This action cannot be undone.`;
        }
    }

    static CloseConfirmDeleteWindow() {
        this.deleteWindow.style.display = "none";
    }

    //Color methods
    static CheckAndChangeTextColor() {
        document.querySelectorAll(".element-block").forEach(el => {
            const style = window.getComputedStyle(el);
            const bg = style.backgroundColor;

            const rgbMatch = bg.match(/\d+/g);
            if (!rgbMatch || rgbMatch.length < 3) return;

            const r = parseInt(rgbMatch[0]).toString(16).padStart(2, '0');
            const g = parseInt(rgbMatch[1]).toString(16).padStart(2, '0');
            const b = parseInt(rgbMatch[2]).toString(16).padStart(2, '0');
            const hex = `${r}${g}${b}`;

            if (this.isDarkColor(hex)) {
                el.style.color = "white";
            } else {
                el.style.color = "black";
            }
        });
    }

    //For changing text color when background is dark
    static isDarkColor(hexColor) {
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

    //Get localization from cookie
    static getCookieValue(name) {
        const cookies = document.cookie.split('; ');
        for (let cookie of cookies) {
            const [key, value] = cookie.split('=');
            if (key === name) {
                return value;
            }
        }
        return null;
    }
}

