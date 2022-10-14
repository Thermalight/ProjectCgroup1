<script>
    export let status;
    let table = {
        1 : "Not handled",
        2 : "Being handled",
        3 : "Handled",
        4 : "False alarm",
    }

    function changeStatus() {
        status += 1;
    }
</script>
<p on:click={changeStatus} class="{table[status]} status absolute right-6 px-2 rounded-full">{table[status]}</p>

<style>
    .status {
        transform: translate(0%, -140%);;
    }
    .Not {
        background-color: #FF4B4B;
    }

    .Being {
        background-color: #FFB951;
    }

    .Handled {
        background-color: #32FF84;
    }

    .False {
        background-color: grey;
    }
</style>