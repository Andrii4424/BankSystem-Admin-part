import { GeneralListMethods } from "../../GeneralListMethods.js";

const submitFilters = document.querySelectorAll(".filter-submit-button");
const entityName = document.getElementById("span-info").dataset.entity;
const firstElementFilter = document.querySelector('input[name="FirstElement"]');
const elementsToLoadFilter = document.querySelector('input[name="ElementsToLoad"]');
const boolFilters = document.querySelectorAll(".bool-filters");
const form = document.getElementById("search");
const sortValue = document.querySelectorAll('input[name="SortValue"]');
const loadMoreButton = document.getElementById("load-banks");
elementsToLoadFilter.value = 6;

loadButtonChecker();

boolFilters.forEach(filter => {
    filter.addEventListener("click", () => {
        filter.value = filter.checked
    });
});

sortValue.forEach(orderBy => {
    orderBy.addEventListener("change", async () => {
        await GetBanks();
    });
});


submitFilters.forEach(button => {
    button.addEventListener("click", async () => {
        await GetBanks();
    })
}); 

async function GetBanks() {
    firstElementFilter.value = 0;
    const formData = new FormData(form)
    document.querySelectorAll(".element-block").forEach(element => element.remove());
    const response = await fetch(`/load-banks/for/${entityName}`, {
        method: "POST",
        body: formData
    });

    await GeneralListMethods.AddElementsToEnd(response);
    loadButtonChecker();
}

loadMoreButton.addEventListener("click", async () => {
    firstElementFilter.value = GeneralListMethods.GetElementsCount();
    elementsToLoadFilter.value = 6;
    const formData = new FormData(form)
    const response = await fetch(`/load-banks/for/${entityName}`, {
        method: "POST",
        body: formData
    });

    await GeneralListMethods.AddElementsToEnd(response);
    elementsToLoadFilter.value = GeneralListMethods.GetElementsCount();
    loadButtonChecker();
})

function loadButtonChecker() {
    const listCount = document.querySelectorAll(".elements-count")[document.querySelectorAll(".elements-count").length - 1].dataset.elementsListCount;
    if (GeneralListMethods.GetElementsCount() >= listCount) {
        loadMoreButton.style.display = "none";
    }
    else {
        loadMoreButton.style.display = "block";
    }
}