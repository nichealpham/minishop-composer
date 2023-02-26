/**
 * utils to detect safari browser
 * @return {bool}
 */
export const isSafariBrowser = function () {
    let sUsrAg = navigator.userAgent;
    if ( sUsrAg.indexOf( 'Safari' ) !== -1 && sUsrAg.indexOf( 'Chrome' ) === -1 )
        return true;
    return false;
}


/**
 * utils to detect Edge browser
 * @return {bool}
 */
export const isEdgeBrowser = function () {
    let sUsrAg = navigator.userAgent;
    if ( sUsrAg.indexOf( "Edge" ) > -1 )
        return true;
    return false;
}

/**
 * get index of the element
 * @param {Element} element 
 */
export const getIndex = function ( element ) {
    let children = element.parentElement.children;
    for ( let i = 0; i < children.length; i++ ) {
        if ( element == children[ i ] ) return i;
    }

    return 0;
}

/**
 * filter products by category.
 * @param {Array} products 
 * @param {Array} category 
 * @param {Boolean} flag 
 */
export const catFilter = function ( products = [], category, flag = false ) {
    if ( category[ 0 ] === 'All' ) return products;

    return products.filter( item => {
        for ( let i = 0; i < category.length; i++ ) {
            if ( item.category.find( cat => cat.slug == category[ i ] ) ) {
                if ( !flag ) return true;
            } else {
                if ( flag ) return false;
            }
        }

        if ( flag )
            return true;
        else
            return false;
    } )
}

/**
 * filter products by attribute
 * @param {Array} products 
 * @param {String} attr 
 */
export const attrFilter = function ( products = [], attr ) {
    return products.filter( item => {
        if ( attr === 'all' ) {
            return true;
        }

        if ( attr === 'sale' && item.sale_price ) {
            return true;
        }

        if ( attr === 'rated' && item.ratings > 3 ) {
            return true;
        }

        if ( attr === 'until' && item.until ) {
            return true;
        }

        return item[ attr ] === true;
    } );
}

export const scrollToPageContent = function () {
    let to = document.querySelector( '.page-content' )
        .offsetTop - 74;
    if ( isSafariBrowser() || isEdgeBrowser() ) {
        let pos = window.pageYOffset;
        let timerId = setInterval( () => {
            if ( pos <= to ) clearInterval( timerId );
            window.scrollBy( 0, -120 );
            pos -= 120;
        }, 1 );
    } else {
        window.scrollTo( {
            top: to,
            behavior: 'smooth'
        } );
    }
}

/**
 * utils to make mobile menu
 */
export const mobileMenu = function () {
    let items = document.querySelector( '.mobile-menu' ).querySelectorAll( 'li' );
    let body = document.querySelector( "body" );

    for ( let i = 0; i < items.length; i++ ) {
        let item = items[ i ];
        if ( item.querySelector( 'ul' ) ) {
            let span = document.createElement( "span" );
            span.className = "mmenu-btn";
            item.querySelector( 'a' ).appendChild( span );
        }

        item.addEventListener( "click", function () {
            if ( body.classList.contains( 'mmenu-active' ) ) {
                body.classList.remove( 'mmenu-active' );
            }
        } );
    }

    items = document.querySelectorAll( ".mmenu-btn" );
    for ( let i = 0; i < items.length; i++ ) {
        let item = items[ i ];

        item.addEventListener( "click", function ( e ) {
            let parent = item.parentElement.parentElement;
            let targetUI = parent.querySelector( "ul" );
            targetUI.setAttribute( "style", "display: block; visibility: hidden;" );

            let targetHeight = targetUI.offsetHeight;
            let delta = targetHeight / 60;

            if ( isEdgeBrowser() ) {
                delta = targetHeight / 30;
            }

            if ( !parent.classList.contains( 'open' ) ) {
                let height = 0;
                let timerID = setInterval( () => {
                    if ( targetHeight <= height ) {
                        targetUI.removeAttribute( "style" );
                        targetUI.setAttribute( "style", "display: block;" );
                        clearInterval( timerID );
                        return null;
                    }

                    targetUI.setAttribute( "style", "display: block; overflow: hidden; height: " + height + 'px' );
                    height += delta;
                }, 1 );

                parent.classList.add( 'open' );
            } else {
                let height = targetHeight;
                let timerID = setInterval( () => {
                    if ( height <= 0 ) {
                        targetUI.removeAttribute( "style" );
                        targetUI.setAttribute( "style", "display: none;" );
                        clearInterval( timerID );
                        return null;
                    }

                    targetUI.setAttribute( "style", "display: block; overflow: hidden; height: " + height + 'px' );
                    height -= delta;
                }, 1 );

                parent.classList.remove( 'open' );
            }

            e.stopPropagation();
            e.preventDefault();
        } );
    }
}