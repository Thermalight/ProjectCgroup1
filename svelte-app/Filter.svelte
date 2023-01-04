<script>
    let open = false;
    let useRange = false;
    let range = -1
    export let notifications;
    export let changed

    function getEvents() {
        fetch("https://" + window.location.host + "/limitnotifications?"+ new URLSearchParams({
            limit: useRange ? range : 10
        }), {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
        })
        .then(response => response.json())
        .then(data => notifications = data)
    }

    function openFilter(){
        open = !open;
    }

    function onSave(){
        notifications = getEvents()
        changed = !changed
    }
</script>

<div>
    <button on:click={openFilter}>settings</button>
    { #if open}
        <div>
            <input type="checkbox" bind:checked={useRange}>
            <input type="range" min="1" max="100" disabled={useRange ? "" : "disabled" } bind:value={range}>
        </div>
        <button on:click={() => onSave()}>Save</button>
    { /if }
</div>
