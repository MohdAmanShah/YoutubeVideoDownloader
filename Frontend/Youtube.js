export default async function Youtube(serverBaseUrl, videoUrl, ResultWrapper) {

    const VideoId = videoUrl.searchParams.get("v").toString();

    const response = await fetch(`${serverBaseUrl}/Youtube/${VideoId}`);
    const restext = await response.text();
    const result = JSON.parse(restext);

    const element = document.createElement('div');
    const Image = document.createElement('img');


    Image.src = "https://img.youtube.com/vi/" + VideoId + "/maxresdefault.jpg";
    Image.classList.add("thumbnail");
    element.appendChild(Image);

    Array.from(result).forEach(e => {

        const button = document.createElement('button');
        button.classList.add(e.FileType, "btn");
        button.innerText = `Download ${e.FileType}`;
        button.addEventListener('click', async ev => {
            const response = await fetch(`${serverBaseUrl}/Youtube/Files/${e.FileName}`, {
                headers: {
                    "Accept": "video/*, audio/*",
                }
            });
            if (!response.ok) {
                console.log("file not found");
                ResultWrapper.appendChild(document.createTextNode('file not found'));
            }
            else {
                const resfile = await response.blob();
                const link = document.createElement('a');

                link.href = window.URL.createObjectURL(resfile);
                link.download = e.FileName;
                link.style.visibility = `hidden`;
                link.style.position = 'fixed';
                document.body.appendChild(link);
                link.click();

                document.body.removeChild(link);
                window.URL.revokeObjectURL(link.href);
            }
        })

        element.appendChild(button);

    })

    element.classList.add("result")
    ResultWrapper.appendChild(element)
}
