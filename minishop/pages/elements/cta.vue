<template>
    <main class="main">
        <page-header title="Call to Action" subtitle="Elements"></page-header>

        <div class="page-content pb-5">
            <div class="container">
                <h2 class="title text-center mb-5">
                    Newsletter Banner
                    <span class="title-separator">/</span> With Background
                </h2>

                <div
                    class="cta cta-horizontal cta-horizontal-box bg-image mb-5"
                    style="background-position: center right;"
                    v-lazy:background-image="
                        './images/backgrounds/cta/bg-1.jpg'
                    "
                >
                    <div class="row align-items-center">
                        <div class="col-lg-4 col-xl-3 offset-xl-1">
                            <h3 class="cta-title">Join Our Newsletter</h3>

                            <p class="cta-desc">
                                Lorem ipsum dolor sit amet adipiscing.
                            </p>
                        </div>

                        <div class="col-lg-8 col-xl-7">
                            <form action="#">
                                <div class="input-group">
                                    <input
                                        type="email"
                                        class="form-control"
                                        placeholder="Enter your Email Address"
                                        aria-label="Email Adress"
                                        required
                                    />
                                    <div class="input-group-append">
                                        <button
                                            class="btn btn-primary btn-rounded"
                                            type="submit"
                                        >
                                            <span>Subscribe</span>
                                            <i
                                                class="icon-long-arrow-right"
                                            ></i>
                                        </button>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>

                <hr class="mb-5" />
                <h2 class="title text-center mb-5">
                    Newsletter Banner
                    <span class="title-separator">/</span> With Dark Background
                </h2>
            </div>

            <parallax
                class="cta bg-image bg-dark pt-6 pb-7 mb-5"
                :bg-image="'./images/backgrounds/bg-large.jpg'"
            >
                <div class="container">
                    <div class="row justify-content-center">
                        <div class="col-sm-10 col-md-8 col-lg-6">
                            <div class="cta-heading text-center">
                                <h3 class="cta-title text-white">
                                    Join Our Newsletter
                                </h3>

                                <p class="cta-desc text-light">
                                    Lorem ipsum dolor sit amet adipiscing.
                                </p>
                            </div>

                            <form action="#">
                                <div class="input-group">
                                    <input
                                        type="email"
                                        class="form-control"
                                        placeholder="Enter your Email Address"
                                        aria-label="Email Adress"
                                        required
                                    />
                                    <div class="input-group-append">
                                        <button
                                            class="btn btn-primary"
                                            type="submit"
                                        >
                                            <span>Subscribe</span>
                                            <i
                                                class="icon-long-arrow-right"
                                            ></i>
                                        </button>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </parallax>

            <hr class="mb-5" />

            <div class="container">
                <h2 class="title text-center mb-5">
                    Newsletter Banner
                    <span class="title-separator">/</span> Without Background
                </h2>

                <div class="cta cta-horizontal mb-3">
                    <div class="row align-items-center">
                        <div class="col-lg-4 col-xl-3 offset-xl-1">
                            <h3 class="cta-title">Join Our Newsletter</h3>

                            <p class="cta-desc">
                                Lorem ipsum dolor sit amet adipiscing.
                            </p>
                        </div>

                        <div class="col-lg-8 col-xl-7">
                            <form action="#">
                                <div class="input-group">
                                    <input
                                        type="email"
                                        class="form-control"
                                        placeholder="Enter your Email Address"
                                        aria-label="Email Adress"
                                        required
                                    />
                                    <div class="input-group-append">
                                        <button
                                            class="btn btn-primary btn-rounded"
                                            type="submit"
                                        >
                                            <span>Subscribe</span>
                                            <i
                                                class="icon-long-arrow-right"
                                            ></i>
                                        </button>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>

                <hr class="mb-5" />

                <h2 class="title text-center mb-5">
                    Call to Action
                    <span class="title-separator">/</span> Newsletter Banner
                    <span class="title-separator">/</span> With Background
                </h2>

                <div
                    class="cta cta-separator bg-image mb-5"
                    style="background-position: center right;"
                    v-lazy:background-image="
                        './images/backgrounds/cta/bg-5.jpg'
                    "
                >
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="cta-wrapper cta-text text-center">
                                <h3 class="cta-title">
                                    Morbi in sem quis dui placerat pellentesque
                                    felis.
                                </h3>

                                <p class="cta-desc">
                                    Donec nec justo eget felis facilisis
                                    fermentum. Aliquam porttitor mauris sit amet
                                    orci.
                                </p>

                                <a href="#" class="btn btn-primary btn-rounded">
                                    <span>Click Here</span>
                                    <i class="icon-long-arrow-right"></i>
                                </a>
                            </div>
                        </div>

                        <div class="col-lg-6">
                            <div class="cta-wrapper text-center">
                                <h3 class="cta-title">Join Our Newsletter</h3>

                                <p class="cta-desc">
                                    Lorem ipsum dolor sit amet adipiscing.
                                </p>

                                <form action="#">
                                    <input
                                        type="email"
                                        class="form-control form-control-rounded"
                                        placeholder="Enter your Email Address"
                                        aria-label="Email Adress"
                                        required
                                    />
                                    <button
                                        class="btn btn-primary"
                                        type="submit"
                                    >
                                        <span>Subscribe</span>
                                        <i class="icon-long-arrow-right"></i>
                                    </button>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>

                <hr class="mt-3 mb-5" />

                <h2 class="title text-center mb-5">
                    Call to Action
                    <span class="title-separator">/</span> Newsletter Banner
                    <span class="title-separator">/</span> No Background
                </h2>

                <div class="cta cta-separator mb-5">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="cta-wrapper cta-text text-center">
                                <h3 class="cta-title">
                                    Morbi in sem quis dui placerat pellentesque
                                    felis.
                                </h3>

                                <p class="cta-desc">
                                    Donec nec justo eget felis facilisis
                                    fermentum. Aliquam porttitor mauris sit amet
                                    orci.
                                </p>

                                <a href="#" class="btn btn-primary btn-rounded">
                                    <span>Click Here</span>
                                    <i class="icon-long-arrow-right"></i>
                                </a>
                            </div>
                        </div>

                        <div class="col-lg-6">
                            <div class="cta-wrapper text-center">
                                <h3 class="cta-title">Join Our Newsletter</h3>

                                <p class="cta-desc">
                                    Lorem ipsum dolor sit amet adipiscing.
                                </p>

                                <form action="#">
                                    <input
                                        type="email"
                                        class="form-control form-control-rounded"
                                        placeholder="Enter your Email Address"
                                        aria-label="Email Adress"
                                        required
                                    />
                                    <button
                                        class="btn btn-primary"
                                        type="submit"
                                    >
                                        <span>Subscribe</span>
                                        <i class="icon-long-arrow-right"></i>
                                    </button>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>

                <hr class="mt-3 mb-5" />

                <h2 class="title text-center mb-5">
                    Call to Action
                    <span class="title-separator">/</span> Newsletter Banner
                    <span class="title-separator">/</span> With Background
                </h2>

                <div class="row">
                    <div class="col-lg-6">
                        <div
                            class="cta bg-image mb-3"
                            style="background-position: center right;"
                            v-lazy:background-image="
                                './images/backgrounds/cta/bg-3.jpg'
                            "
                        >
                            <div class="cta-wrapper cta-text text-center">
                                <h3 class="cta-title">
                                    Morbi in sem quis dui placerat pellentesque
                                    felis.
                                </h3>

                                <p class="cta-desc">
                                    Donec nec justo eget felis facilisis
                                    fermentum. Aliquam porttitor mauris sit amet
                                    orci.
                                </p>

                                <a href="#" class="btn btn-primary btn-rounded">
                                    <span>Click Here</span>
                                    <i class="icon-long-arrow-right"></i>
                                </a>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-6">
                        <div
                            class="cta bg-image mb-3"
                            style="background-position: center right;"
                            v-lazy:background-image="
                                './images/backgrounds/cta/bg-4.jpg'
                            "
                        >
                            <div class="cta-wrapper text-center">
                                <h3 class="cta-title">Join Our Newsletter</h3>

                                <p class="cta-desc">
                                    Lorem ipsum dolor sit amet adipiscing.
                                </p>

                                <form action="#">
                                    <input
                                        type="email"
                                        class="form-control form-control-rounded"
                                        placeholder="Enter your Email Address"
                                        aria-label="Email Adress"
                                        required
                                    />
                                    <button
                                        class="btn btn-primary"
                                        type="submit"
                                    >
                                        <span>Subscribe</span>
                                        <i class="icon-long-arrow-right"></i>
                                    </button>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>

                <hr class="mt-3 mb-5" />
            </div>

            <div class="container">
                <h2 class="title text-center mb-5">
                    Sale Banner
                    <span class="title-separator">/</span> With Background
                </h2>

                <div class="cta cta-border mb-5">
                    <div class="row justify-content-center">
                        <div class="col-md-11 col-xl-10">
                            <div class="cta-content">
                                <div class="cta-heading">
                                    <h3 class="cta-title text-right">
                                        Vivamus vestibulum
                                        <br />ntulla nec ante
                                    </h3>
                                </div>

                                <div class="cta-text">
                                    <p>
                                        Morbi in sem quis dui placerat ornare.
                                        Pellentesque odio nisi, euismod in,
                                        pharetra a, ultricies in, diam. Sed
                                        arcu. Cras consequat.
                                    </p>
                                </div>

                                <a href="#" class="btn btn-primary btn-rounded">
                                    <span>Click Here</span>
                                    <i class="icon-long-arrow-right"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>

                <hr class="mb-5" />
                <h2 class="title text-center mb-5">
                    Sale Banner
                    <span class="title-separator">/</span> With Background
                </h2>

                <div
                    class="cta bg-image pt-6 pb-7 mb-5"
                    style="background-position: center right;"
                    v-lazy:background-image="
                        './images/backgrounds/cta/bg-2.jpg'
                    "
                >
                    <div class="row justify-content-center">
                        <div class="col-sm-10 col-md-8 col-lg-6">
                            <div class="cta-text text-center">
                                <h3 class="cta-title">
                                    Morbi in sem quis dui placerat felis.
                                </h3>

                                <p class="cta-desc">
                                    Morbi in sem quis dui placerat ornare.
                                    Pellentesque odio nisi, euismod in, pharetra
                                    a, ultricies in, diam. Sed arcu. Cras
                                    consequat.
                                </p>

                                <a href="#" class="btn btn-primary btn-rounded">
                                    <span>Click Here</span>
                                    <i class="icon-long-arrow-right"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>

                <hr class="mb-5" />
                <h2 class="title text-center mb-5">
                    Sale Banner
                    <span class="title-separator">/</span> With Dark Background
                </h2>
            </div>

            <parallax
                class="cta bg-image bg-dark pt-6 pb-7"
                :bg-image="'./images/backgrounds/bg-large.jpg'"
            >
                <div class="container">
                    <div class="row justify-content-center">
                        <div class="col-sm-10 col-md-8 col-lg-6">
                            <div class="cta-text text-center">
                                <h3 class="cta-title text-white">
                                    Morbi in sem quis dui placerat felis.
                                </h3>

                                <p class="cta-desc text-light">
                                    Morbi in sem quis dui placerat ornare.
                                    Pellentesque odio nisi, euismod in, pharetra
                                    a, ultricies in, diam. Sed arcu. Cras
                                    consequat.
                                </p>

                                <a href="#" class="btn btn-primary btn-rounded">
                                    <span>Click Here</span>
                                    <i class="icon-long-arrow-right"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </parallax>
        </div>

        <element-list></element-list>
    </main>
</template>

<script>
import PageHeader from '~/components/elements/PageHeader';
import Parallax from '~/components/elements/Parallax';
import ElementList from '~/components/partial/elements/ElementList';

export default {
    components: {
        Parallax,
        PageHeader,
        ElementList
    }
};
</script>
