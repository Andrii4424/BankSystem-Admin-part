export class GeneralListMethods {
    //General values
    static emptyListTitle = document.getElementById("empty-list-title");
    static radioInputs = document.querySelectorAll('input[name="sort"]');
    static elementsListBlock = document.getElementById("elements-block");
    static filterList = document.getElementById("filter-list");
    static searchInput = document.getElementById("search-input");
    static oneColumn = document.getElementById("one-column");
    static twoColumns = document.getElementById("two-columns");
    static lastSearch;

    //Checkers
    //Checks whether to display a message about an empty list
    static EmptyListTitleChecker(propertyName) {
        propertyName = propertyName.toLowerCase()
        if (this.GetElementsCount() === 0 && this.CheckForFilters()) {
            this.emptyListTitle.style.display = "block"
            this.emptyListTitle.textContent = `No ${propertyName}s matched your search. You can create a new ${propertyName} by clicking the button below`;
        }
        else if (this.GetElementsCount() === 0 && !this.CheckForFilters()) {
            this.emptyListTitle.style.display = "block"
            const titlePropertyName = propertyName[0].toUpperCase() + propertyName.slice(1);
            this.emptyListTitle.textContent = `${titlePropertyName} list is empty, you can create ${propertyName} by button bellow`;
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
        if (Array.from(filters).some(filter => filter.checked)) return true;
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
        localStorage.setItem("lastSearch", this.lastSearch);
        return this.lastSearch;
    }

    static GetLastSearch() {
        return localStorage.getItem("lastSearch");
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

