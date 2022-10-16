<script>
    export let status;
    let table = {
        1 : "Not handled",
        2 : "Being handled",
        3 : "Handled",
        4 : "False alarm",
    }

    function getDictLength(dict) {
        var count = 0;
        for (var item in dict) {
            if (dict.hasOwnProperty(item)) count++;
        }
        return count
    }

    function changeStatus() {
        status = 1 + status % getDictLength(table);
        // TODO: request database update with current status
    }
</script>
<p on:click|stopPropagation={changeStatus} class="{table[status]} status absolute right-6 px-2 rounded-full">{table[status]}</p>

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