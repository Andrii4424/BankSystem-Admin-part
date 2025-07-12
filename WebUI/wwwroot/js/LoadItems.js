//Load elements
//General DOM elements
const elementsListBlock = document.getElementById("elements-block");
const sortButton = document.getElementById("sort-icon");
const filterButton = document.getElementById("filter-icon");
const submitFilters = document.getElementById("submit-filters");
const searchInput = document.getElementById("search-input");
const searchButton = document.getElementById("search-button");

//Bank general elements
const loadBanks = document.getElementById("load-banks");
const addBank = document.querySelector(".add-element");
let count = document.querySelectorAll(".element-table").length;
let elementsListCount = parseInt(elementsListBlock.dataset.elementsListCount);

//Sort elements
//Bank elements
const sortList = document.getElementById("banks-sort-list");
const radioInputs = document.querySelectorAll('input[name="sort"]');
let orderMethod = elementsListBlock.dataset.orderMethod;

//Filter elements
//Bank filters
const filterList = document.getElementById("banks-filter-list");
const filterSetting = filterList.querySelectorAll('input[type="checkbox"][name="filter"]');
const inputRating = document.querySelector('.rating');

//Input settings
//Bank inputs
inputRating.addEventListener('input', () => {
    if (inputRating.value > 5) inputRating.value = 5;
    if (inputRating.value < 1) inputRating.value = null;
    if (inputRating.value.length > 3) {
        inputRating.value = inputRating.value.slice(0, 3);
    }
});
//Icons handlers
//Sort and filrer icons
filterButton.addEventListener("click", () => {
    filterList.classList.toggle("opened");
    sortList.classList.remove("opened");
});

sortButton.addEventListener("click", () => {
    sortList.classList.toggle("opened");
    filterList.classList.remove("opened");
});


//General methods
function getSortUrl() {
    return document.querySelector('input[name="sort"]:checked').value;
}
function getSearchUrl() {
    return (searchInput.value !== "" && searchInput.value !== null)? searchInput.value: "0";
}

async function addElementsToEnd(response) {
    elementsListBlock.insertAdjacentHTML("beforeend", await response.text());
}

//Filters sort and search handlers
//Bank handlers
//Request handlers
radioInputs.forEach(async function (button) {
    button.addEventListener("change", async () => {
        await sortAndFilterBanks();
    });
});

submitFilters.addEventListener("click", async () => {
    await sortAndFilterBanks();
});

searchButton.addEventListener("click", async () => {
    await sortAndFilterBanks();
});

async function sortAndFilterBanks() {
    const url = `/load-banks/0/${count}/${getSearchUrl()}/${getSortUrl()}${getBankFiltersUrl()}`
    const response = await fetch(url, {
        method: "GET"
    });
    document.querySelectorAll(".element-block").forEach(element => element.remove());
    await addElementsToEnd(response);
}

//Additional bank filtration handler methods
filterSetting.forEach(button => {
    button.addEventListener("change", () => {
        const inputValue = filterList.querySelector(`input[type="number"][class="${button.value}"]`);
        if (inputValue !== null) {
            inputValue.value = null;
            inputValue.disabled = !button.checked;
        }
    });
});


function getBankFiltersUrl() {
    const filters = filterList.querySelectorAll(`input[type="checkbox"][name="filter"]`);
    let url = "";
    filterList.querySelectorAll(`input[type="checkbox"][class="value-filter"]`).forEach(filter => {
        if (filter.checked) {
            let filterValue = filterList.querySelector(`input[type="number"][class="${filter.value}"]`).value;
            filter.checked = (filterValue !== null && filterValue!=="");
        }
    });

    filters.forEach(filter => {
        switch (filter.value) {
            case "license-required":
                url += "/" + filter.checked;
                break;
            case "site-required":
                url += "/" + filter.checked;
                break;
            case "rating":
                filter.checked ? url += "/" + filterList.querySelector(`input[type="number"][class="${filter.value}"]`).value : url += "/0";
                break;
            case "clients-count":
                filter.checked ? url += "/" + filterList.querySelector(`input[type="number"][class="${filter.value}"]`).value : url += "/0";
                break;
            case "capitalization":
                filter.checked ? url += "/" + filterList.querySelector(`input[type="number"][class="${filter.value}"]`).value : url += "/0";
                break;
            default:
                break;
        }
    })
    return url;
}


//Pagination methods
//Bank methods
if (elementsListCount == count) {
    loadBanks.style.display = "none";
}

if (orderMethod!==null && orderMethod!==undefined && orderMethod!=="") {
    radioInputs.forEach(button => {
        if(button.value===orderMethod) {
            button.checked = true;
        }
    });
}


loadBanks.addEventListener("click", async () => {
    await LoadMore("/load-banks/" + count +"/"+6 +"/"+ document.querySelector('input[name="sort"]:checked').value);
});


async function LoadMore(url) {
    const response = await fetch(url, {
        method: "GET"
    });
    await addElementsToEnd(response);

    count = document.querySelectorAll(".element-table").length;
    if (elementsListCount == count) {
        loadBanks.style.display = "none";
    }
}

addBank.addEventListener("click", () => {
    const input = document.getElementById("add-bank-input");
    const inputMethod = document.getElementById("add-bank-input-method");
    let count = document.querySelectorAll(".element-table").length;
    input.value = count;
    inputMethod.value = document.querySelector('input[name="sort"]:checked').value;
});

elementsListBlock.addEventListener("click", (event) => {
    if (event.target.matches(".update-element")) {
        let count = document.querySelectorAll(".element-table").length;
        event.target.closest("form").querySelector(".update-bank-input").value = count;
        const method = document.querySelector('input[name="sort"]:checked').value
        event.target.closest("form").querySelector(".update-bank-order-method").value = method;
    }
})

//Decraese when element is deleting
elementsListBlock.addEventListener("click", async (event) => {
    if (event.target.matches(".delete-element")) {
        elementsListCount--;
        if (elementsListCount == count) {
            loadBanks.style.display = "none";
        }
    }
});
