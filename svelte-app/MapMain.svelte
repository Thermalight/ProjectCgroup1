<script >
import L from "leaflet";
import { onMount } from 'svelte';
import List from './List.svelte';
import { slide } from 'svelte/transition';
import Notification from "./Notification.svelte"

export let mapComponent
let notifications;
let loading = true;
let refreshRate = 1000;
let statusName = "Not handled";

let clear
$: {
    clearInterval(clear)
    clear = setInterval(getEvents, refreshRate)
}

function getEvents() {
    fetch("https://localhost/notifications", {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => notifications = data)
        .then(() => {
            loading = false;
            refreshRate = 1000;
        })
        .catch((e) => {
            console.log(e);
            refreshRate = 10000;
        })
        .finally
}
let mapContainer;
let interval;
const returnNada = () => '';
let map = L.map(L.DomUtil.create("div"), {
    center: [0, 0],
    zoom: 0,
}).setView([-0.6955588075932391, 22.583414923387544],7);

// export async function loadNotifications() { 
//     const response = await fetch("https://localhost/notifications", {
//         method: "GET",
//         headers: {
//             "Content-Type": "application/json",
//         },
//     });
//     return response.json();
// }

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
    // if (marker != null) {
    //     map.removeLayer(marker)
    // }
    // marker = L.marker([Latitude, Longitude]).addTo(map);
    // marker.bindPopup(Latitude+", "+Longitude);
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
    {#if notifications != null && !loading}
        {#each notifications as notification}
            {#if notification.StatusID == 1}
                {statusName = "Not handled"}
            {:else if notification.StatusID == 2}
                {statusName = "Being handled"}
            {:else if notification.StatusID == 3}
                {statusName = "Handled"}
            {:else if notification.StatusID == 4}
                {statusName = "False alarm"}
            {:else}
                {statusName = "Not handled"}
            {/if}
            {returnNada((L.marker([notification.Latitude, notification.Longitude]).bindPopup(notification.sound_type+" with probablity of "+notification.Probability+"% status is "+statusName)).addTo(map))}
        {/each}
    {/if}

</div>

