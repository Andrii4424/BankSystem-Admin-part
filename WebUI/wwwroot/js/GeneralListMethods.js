export class GeneralListMethods {
    //General values
    static emptyListTitle = document.getElementById("empty-list-title");
    static radioInputs = document.querySelectorAll('input[name="sort"]');
    static elementsListBlock = document.getElementById("elements-block");
    static filterList = document.getElementById("filter-list");
    static searchInput = document.getElementById("search-input");

    //checkers
    static EmptyListTitleChecker() {
        if (this.GetElementsCount() === 0) { this.emptyListTitle.style.display = "block" }
        else { this.emptyListTitle.style.display = "none" }
    }

    //Getters
    static GetElementsCount() {
        let x = document.querySelectorAll(".element-table").length;
        return  document.querySelectorAll(".element-table").length;
    }

    static GetElementsAddCount() {
        return (document.querySelectorAll(".element-table").length) > 6 ? document.querySelectorAll(".element-table").length : 6;
    }

    static GetSortUrl() {
        return document.querySelector('input[name="sort"]:checked').value;
    }
    static GetSearchUrl() {
        return (this.searchInput.value !== "" && this.searchInput.value !== null) ? this.searchInput.value : "0";
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
}

