<template>
    <main class="main">
        <page-header title="Video Banners" subtitle="Elements"></page-header>

        <div class="page-content">
            <div class="container">
                <h2 class="title mb-3 text-center">Fullwidth Banner</h2>
            </div>

            <div
                class="video-banner video-banner-bg bg-image text-center"
                v-lazy:background-image="'./images/video/bg-1.jpg'"
            >
                <div class="container">
                    <h3 class="video-banner-title h1 text-white">
                        <span>New Video</span>
                        <strong>Womens New Arrivals</strong>
                    </h3>

                    <a
                        href="https://www.youtube.com/watch?v=vBPgmASQ1A0"
                        @click.prevent="openVideoModal"
                        class="btn-video btn-iframe"
                    >
                        <i class="icon-play"></i>
                    </a>
                </div>
            </div>

            <div class="container">
                <hr class="mt-5 mb-4" />
                <h2 class="title mb-3 text-center">
                    Video Banner with Description
                </h2>
            </div>

            <div class="video-banner video-banner-poster text-center">
                <div class="container">
                    <div class="row align-items-center">
                        <div class="col-md-6 mb-3 mb-md-0">
                            <h3 class="video-banner-title h3">
                                <span class="text-primary">New Video</span
                                >Womens New Arrivals
                            </h3>

                            <p>
                                Lorem ipsum dolor sit amet, consectetuer
                                adipiscing elit. Donec odio. Quisque volutpat
                                mattis eros. Nullam malesuada erat ut turpis.
                                Suspendisse urna nibh, viverra non, semper ...
                            </p>
                        </div>

                        <div class="col-md-6">
                            <div class="video-poster">
                                <img
                                    v-lazy="'./images/video/poster-1.jpg'"
                                    alt="poster"
                                />

                                <div class="video-poster-content">
                                    <a
                                        href="https://www.youtube.com/watch?v=vBPgmASQ1A0"
                                        @click.prevent="openVideoModal"
                                        class="btn-video btn-iframe"
                                    >
                                        <i class="icon-play"></i>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="container">
                <hr class="mt-5 mb-4" />
                <h2 class="title mb-3 text-center">
                    Video Banner with Background
                </h2>
            </div>

            <div
                class="video-banner bg-image text-center pt-8 pb-8"
                v-lazy:background-image="'./images/video/bg-2.jpg'"
            >
                <div class="container">
                    <div class="row">
                        <div class="col-sm-10 offset-sm-1 col-md-8 offset-md-2">
                            <div class="video-poster">
                                <img
                                    v-lazy="'./images/video/poster-2.jpg'"
                                    alt="poster"
                                />

                                <div class="video-poster-content">
                                    <h3
                                        class="h4 video-poster-title text-white"
                                    >
                                        Womens New Arrivals
                                    </h3>

                                    <a
                                        href="https://www.youtube.com/watch?v=vBPgmASQ1A0"
                                        @click.prevent="openVideoModal"
                                        class="btn-video btn-iframe"
                                    >
                                        <i class="icon-play"></i>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="container">
                <hr class="mt-5 mb-4" />
                <h2 class="title mb-3 text-center">Deal Video Banner</h2>
            </div>

            <div class="video-banner bg-light pt-5 pb-5">
                <div class="container align-items-center">
                    <div class="video-banner-box bg-white">
                        <div class="row align-items-center">
                            <div class="col-md-6 mb-3 mb-md-0">
                                <div class="video-box-content">
                                    <h3 class="video-banner-title h1">
                                        <span class="text-primary"
                                            >New Video</span
                                        >
                                        <strong>Deal Banner</strong>
                                    </h3>

                                    <p>
                                        Lorem ipsum dolor sit amet, consecte
                                        adipiscing elit. Donec odio. Quisque
                                        volutpat mattis eros. Nullam malesuada
                                        erat ut turpis.
                                    </p>
                                    <a href="#" class="btn btn-outline-primary">
                                        <span>Click Here</span>
                                        <i class="icon-long-arrow-right"></i>
                                    </a>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="video-poster">
                                    <img
                                        v-lazy="'./images/video/poster-3.jpg'"
                                        alt="poster"
                                    />

                                    <div class="video-poster-content">
                                        <a
                                            href="https://www.youtube.com/watch?v=vBPgmASQ1A0"
                                            @click.prevent="openVideoModal"
                                            class="btn-video btn-iframe"
                                        >
                                            <i class="icon-play"></i>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <element-list></element-list>
    </main>
</template>

<script>
import PageHeader from '~/components/elements/PageHeader';
import ElementList from '~/components/partial/elements/ElementList';

export default {
    components: {
        PageHeader,
        ElementList
    },
    methods: {
        openVideoModal: function() {
            this.$modal.show(
                () => import('~/components/elements/modals/VideoModal'),
                {},
                { width: '1060', height: '596', adaptive: true }
            );
        }
    }
};
</script>
