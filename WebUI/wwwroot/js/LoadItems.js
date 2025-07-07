const loadBanks = document.getElementById("load-banks");
const elementsBlock = document.getElementById("elements-block");
const elementsListCount = parseInt(elementsBlock.dataset.elementsListCount);

loadBanks.addEventListener("click", async () => {
    let count = document.querySelectorAll(".element-table").length;
    const response = await LoadMore("/load-banks/"+ count);
    elementsBlock.insertAdjacentHTML("beforeend", response);

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