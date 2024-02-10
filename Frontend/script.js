import Youtube from "./Youtube.js";

// https://www.youtube.com/watch?v=Oextk-If8HQ&pp=ygUWc29tZXdoZXJlIG9ubHkgd2Uga25vdw%3D%3D
// https://open.spotify.com/track/5CFZEWl1VQpFDda3PYitiP?si=1abf95745ddf445b

const serverBaseUrl = "https://localhost:44366";
const Form = document.getElementById("Search-Form");
const SearchInput = document.getElementById("Search");
const ResultWrapper = document.getElementById("Results");
const SearchButton = document.querySelector("[data-search-button]");


Form.addEventListener("submit", (e) => {
    e.preventDefault();
    Handler();
});

async function Handler() {

    ResultWrapper.innerHTML = '';
    SearchButton.innerText = 'Processing Video....';
    SearchButton.disabled = true;

    const url = new URL(SearchInput.value || "https://www.youtube.com/watch?v=Oextk-If8HQ&pp=ygUWc29tZXdoZXJlIG9ubHkgd2Uga25vdw%3D%3D");
    await UrlHandler(url);

    SearchButton.disabled = false;
    SearchButton.innerText = 'Search';
}

async function UrlHandler(url) {
    let host = url.host

    switch (host) {
        case "www.youtube.com":
            await Youtube(serverBaseUrl, url, ResultWrapper);
            break;

        case "open.spotify.com":
            console.log("Requesting Spotify");
            break;

        default:
            console.log("undefined Request");
            break;
    }
}