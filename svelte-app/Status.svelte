<script>
    export let status;
    export let GUID;

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
        fetch("https://" + window.location.host + `/notifications?id=${GUID}&status=${status}`, {
            headers: {
                'Content-Type': 'application/json',
                "Authorization": 'Bearer ' + localStorage.getItem("token")
            },
            method: 'POST',
        });
    }
</script>
<p on:click|stopPropagation={changeStatus} class="{table[status]} status absolute -right-1 top-8 px-2 rounded-full">{table[status]}</p>

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