const loadBanks = document.getElementById("load-banks");

loadBanks.addEventListener("click", async () => {
    const elements = document.getElementById("elements-block");
    let count = document.querySelectorAll(".element-table").length;
    const response = await LoadMore("/load-banks/" + count);
    const elementsListCount = parseInt(elements.dataset.elementsListCount);
    elements.insertAdjacentHTML("beforeend", response);

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