function confirmDelete(gameId) {
    Swal.fire({
        title: 'Are you sure that you want to delete this game?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#bb2124',
        cancelButtonColor: '#a5bbc8',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Games/Delete/${gameId}`;
        }
    });
    return false;
}