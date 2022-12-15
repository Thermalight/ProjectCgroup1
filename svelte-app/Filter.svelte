<script>
    let open = false;
    let useRange = false;
    let range = -1
    export let notifications;
    export let changed
    export let filterBool = false
    export let handleSubmit
    
    const returnNada = () => '';
    function getEvents() {
        fetch("https://localhost/limitnotifications?"+ new URLSearchParams({
            limit: useRange ? range : 10
        }), {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
        })
        .then(response => response.json())
        .then(data => notifications = data)
        .then(() => changed = true)

    }

    function openFilter(){
        open = !open;
    }
    
    function onSave(){
        notifications = getEvents()
        changed = !changed
        filterBool = true
    }
</script>

<div>
    <button class="bg-white" on:click={openFilter}>settings</button>
    { #if changed && filterBool }
        {returnNada(handleSubmit())}
    { /if }
    { #if open}
        <div>
            <input type="checkbox" bind:checked={useRange}>
            <input type="range" min="1" max="10" disabled={useRange ? "" : "disabled" } bind:value={range}>
        </div>
        <button class="bg-white" on:click={() => onSave()}>Reload</button>
    { /if }
</div>
