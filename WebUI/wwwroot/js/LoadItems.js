//Load elements
const loadBanks = document.getElementById("load-banks");
const addBank = document.querySelector(".add-element");
let elementsDiv = document.getElementById("elements-block");
let count = document.querySelectorAll(".element-table").length;
//Sort elements
const sortButton = document.getElementById("sort-icon");
const sortList = document.getElementById("banks-sort-list");


//Load methods
if (document.querySelectorAll(".element-table").length == 0) {
    loadBanks.style.display = "none";
}


loadBanks.addEventListener("click", async () => {
    const elements = document.getElementById("elements-block");
    const response = await LoadMore("/load-banks/" + count);
    const elementsListCount = parseInt(elements.dataset.elementsListCount);
    elements.insertAdjacentHTML("beforeend", response);
    count--;

    count = document.querySelectorAll(".element-table").length;
    if (elementsListCount == count) {
        loadBanks.style.display = "none";
    }
});


async function LoadMore(url) {
    const response = await fetch(url, {
        method: "GET"
    });
    return await response.text();
}

addBank.addEventListener("click", () => {
    const input = document.getElementById("add-bank-input");
    let count = document.querySelectorAll(".element-table").length;
    input.value = count;
});

elementsDiv.addEventListener("click", (event) => {
    if (event.target.matches(".update-element")) {
        let count = document.querySelectorAll(".element-table").length;
        event.target.closest("form").querySelector(".update-bank-input").value = count;
    }
})

//sort methods
sortButton.addEventListener("click", () => {
    sortList.classList.toggle("opened");

});