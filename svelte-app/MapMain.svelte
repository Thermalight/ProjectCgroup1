<script>
import L from "leaflet";
import { setContext, onMount } from "svelte";

let mapContainer;

let map = L.map(L.DomUtil.create("div"), {
    center: [0, 0],
    zoom: 0,
}).setView([-0.6955588075932391, 22.583414923387544],13);

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
</script>

<style>
.map {
    height: 100%;
    width: 100%;
}
</style>
<div class="map rounded-lg" bind:this="{mapContainer}">
</div>

