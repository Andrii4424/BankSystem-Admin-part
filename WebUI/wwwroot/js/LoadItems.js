//Load elements
const loadBanks = document.getElementById("load-banks");
const addBank = document.querySelector(".add-element");
let elementsDiv = document.getElementById("elements-block");
let count = document.querySelectorAll(".element-table").length;

//Sort elements
const sortButton = document.getElementById("sort-icon");
const sortList = document.getElementById("banks-sort-list");
const radioInputs = document.querySelectorAll('input[name="sort"]');

//Load methods
if (document.querySelectorAll(".element-table").length == 0) {
    loadBanks.style.display = "none";
}


loadBanks.addEventListener("click", async () => {
    await LoadMore("/load-banks/" + count +"/"+6 +"/"+ document.querySelector('input[name="sort"]:checked').value);
});


async function LoadMore(url) {
    const elements = document.getElementById("elements-block");
    const response = await fetch(url, {
        method: "GET"
    });
    const elementsListCount = parseInt(elements.dataset.elementsListCount);
    await addElementsToEnd(response);

    count = document.querySelectorAll(".element-table").length;
    if (elementsListCount == count) {
        loadBanks.style.display = "none";
    }
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

radioInputs.forEach(async function(button) {
    button.addEventListener("change", async () => {
        let x = button.value;
        let url = "/load-banks/0/"+count+"/"+button.value;
        orderElements(url);
    });
});

async function orderElements(url) {
    const response = await fetch(url, {
        method: "GET"
    });
    document.querySelectorAll(".element-block").forEach(element => element.remove());
    addElementsToEnd(response);
};

async function addElementsToEnd(response) {
    const elements = document.getElementById("elements-block");
    elements.insertAdjacentHTML("beforeend", await response.text());
}

//Decraese when element is deleting
document.getElementById("elements-block").addEventListener("click", async (event) => {
    if (event.target.matches(".delete-element")) {
        count--;
    }
});
