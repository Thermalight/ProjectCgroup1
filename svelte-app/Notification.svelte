<script>
    export let event;
    let time = new Date(event.Time*1000)
</script>

{#if event != null}
    <div class="notification {event.sound_type}">
        <p>{event.sound_type}</p>
        <p>latitude: {Math.round(event.Latitude*100000)/100000}</p>
        <p>longitude: {Math.round(event.Longitude*100000)/100000}</p>
        <p class="text-right">{time.getHours()}:{time.getMinutes()}</p>
    </div>
{/if}

<style>
    .notification {
        padding: 15px;
        color: white;
        background-color: #202938;
        border-radius: 16px;
        margin-bottom: 5px;
    }

    .gunshot {
        border-bottom: 2px solid red;
    }

    .vehicle {
        border-bottom: 2px solid orange;
    }

    .animal {
        border-bottom: 2px solid yellow;
    }

    .unknown {
        border-bottom: 2px solid grey;
    }
</style>
