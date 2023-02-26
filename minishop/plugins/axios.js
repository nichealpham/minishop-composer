export default function ( { $axios, redirect } ) {
    $axios.onError( error => {
        const code = parseInt( error.response && error.response.status );
        if ( code === 400 ) {
            redirect( '/pages/404' );
        }
    } );
}
