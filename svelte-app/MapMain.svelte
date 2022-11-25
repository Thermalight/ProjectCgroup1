<script >
import L from "leaflet";
import { onMount } from 'svelte';
import { redIcon, orangeIcon, yellowIcon, greyIcon } from './leaflet-color-markers'

let notifications;

let loading = true;
let refreshRate = 1000;
let statusName;
let array = []
let arrayPins = []
let mapDict = {};
let statusDict = {};
let clear
let marker 
let color = greyIcon;

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
const returnNada = () => '';
let map = L.map(L.DomUtil.create("div"), {
    center: [0, 0],
    zoom: 0,
}).setView([-0.6955588075932391, 22.583414923387544],7);

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


export function flyToLocation(Latitude,Longitude){
    map.flyTo([Latitude, Longitude],9);
}

function removeAllLocations(){
    if (arrayPins != null){
        arrayPins.forEach(element => {
            map.removeLayer(element)
            array = []
            arrayPins = []
            mapDict = {};
            statusDict = {};
        });
    }
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
            {#if color == null}
                {color = greyIcon}
            {/if}
            {#if array.includes(notification.Guid) && statusDict[notification.Guid] != statusName  && statusName != null}
                {mapDict[notification.Guid].getPopup().setContent(notification.sound_type+" with probablity of "+notification.Probability+"% status is "+statusName).update()}
                {statusDict[notification.Guid] = statusName}
            {/if}
            {#if notification.sound_type == "gunshot"}
                {color = redIcon}
            {:else if notification.sound_type == "animal"}
                {color = yellowIcon}
            {:else if notification.sound_type == "vehicle"}
                {color = orangeIcon}
            {:else}
                {color = greyIcon}
            {/if}
            {#if !array.includes(notification.Guid)}
                {returnNada(marker = (L.marker([notification.Latitude, notification.Longitude],{icon: color}).bindPopup(notification.sound_type+" with probablity of "+notification.Probability+"% status is "+statusName)).addTo(map))}
                {returnNada(arrayPins.push(marker))}
                {returnNada(array.push(notification.Guid))}
                {returnNada(mapDict[notification.Guid] = marker)}
                {returnNada(statusDict[notification.Guid] = statusName)}
            {/if}
        {/each}
        {/if}

</div>

