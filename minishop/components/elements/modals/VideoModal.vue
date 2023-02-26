<template>
    <div class="video-modal bg-white m-3" id="video-modal">
        <button class="close" type="button" @click="$emit('close')">
            <span>
                <i class="icon-close"></i>
            </span>
        </button>
        <iframe
            src="//www.youtube.com/embed/vBPgmASQ1A0?autoplay=1"
            frameborder="0"
            title="youtube"
            class="mfp-iframe"
        ></iframe>
    </div>
</template>