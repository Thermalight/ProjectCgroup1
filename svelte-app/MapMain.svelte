<script >
import L from "leaflet";
import onMount  from "svelte";

let mapContainer;
const returnNada = () => '';
let map = L.map(L.DomUtil.create("div"), {
    center: [0, 0],
    zoom: 0,
}).setView([-0.6955588075932391, 22.583414923387544],7);

export async function loadNotifications () { 
const response = await fetch("https://localhost/notifications", {
    method: "GET",
    headers: {
        "Content-Type": "application/json",
    },
});
return response.json();
}
    
    


L.tileLayer('https://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}').addTo(map);

onMount(() => {
    mapContainer.appendChild(map.getContainer());
    map.getContainer().style.width = "100%";
    map.getContainer().style.height = "100%";
    map.getContainer().style.class = "rounded-lg";
    map.invalidateSize();
    document.getElementsByClassName( 'leaflet-control-attribution' )[0].style.display = 'none';
    document.getElementsByClassName( 'leaflet-control-zoom' )[0].style.display = 'none';
});

var marker 
export function addLocation(Latitude,Longitude){
    if (marker != null) {
        map.removeLayer(marker)
    }
    marker = L.marker([Latitude, Longitude]).addTo(map);
    marker.bindPopup(Latitude+", "+Longitude);
    map.setView([Latitude, Longitude],9);
}

</script>

<style>
    .map {
        height: 100%;
        width: 100%;
    }
</style>
<div class="map rounded-lg" bind:this="{mapContainer}">
    {#await loadNotifications()}
    {:then data}
        {#each data as notification}
            {returnNada((L.marker([notification.Latitude, notification.Longitude]).bindPopup(notification.Latitude+", "+notification.Longitude)).addTo(map))}
        {/each}
    {/await}
</div>

