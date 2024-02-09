import Youtube from "./Youtube";
Youtube();

const dummyYoutubeLink = "https://www.youtube.com/watch?v=Oextk-If8HQ&list=RDOextk-If8HQ&start_radio=1";
const dummySpotifyLink = "https://open.spotify.com/track/5CFZEWl1VQpFDda3PYitiP?si=1abf95745ddf445b"
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

    //const url = new URL(SearchInput.value);
    const url = new URL(dummySpotifyLink);
    UrlHandler(url);


    //const VideoId = url.searchParams.get("v").toString();
    //const response = await fetch(`https://localhost:44366/Audio/${VideoId}`);
    //const restext = await response.text();
    //const element = document.createElement('div');
    //const Image = document.createElement('img');
    //element.innerText = restext;
    //Image.src = "https://img.youtube.com/vi/" + VideoId + "/maxresdefault.jpg";
    //Image.classList.add("thumbnail");
    //element.appendChild(Image);
    //ResultWrapper.appendChild(element)

    SearchButton.disabled = false;
    SearchButton.innerText = 'Search';
}

function UrlHandler(url) {
    console.log(url);
}