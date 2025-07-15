export class GeneralListMethods {
    //General values
    static emptyListTitle = document.getElementById("empty-list-title");
    static radioInputs = document.querySelectorAll('input[name="sort"]');
    static elementsListBlock = document.getElementById("elements-block");
    static filterList = document.getElementById("filter-list");
    static searchInput = document.getElementById("search-input");
    static oneColumn = document.getElementById("one-column");
    static twoColumns = document.getElementById("two-columns");

    //checkers
    static EmptyListTitleChecker() {
        if (this.GetElementsCount() === 0) { this.emptyListTitle.style.display = "block" }
        else { this.emptyListTitle.style.display = "none" }
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
            this.oneColumn.classList.add = "chosen";
            this.twoColumns.classList.remove = "chosen";
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

