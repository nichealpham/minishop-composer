<template>
    <main class="main element-banner-page">
        <page-header title="Banners" subtitle="Elements"></page-header>

        <div class="page-content">
            <div class="container">
                <h2 class="title text-center mb-3">2 Columns</h2>

                <div class="row">
                    <div class="col-md-6">
                        <div class="banner">
                            <a href="javascript:;">
                                <img
                                    v-lazy="'./images/banners/banner-1.jpg'"
                                    alt="Banner"
                                    width="570"
                                    height="280"
                                />
                            </a>

                            <div class="banner-content">
                                <h4 class="banner-subtitle">
                                    Quisque a lectus
                                </h4>

                                <h3 class="banner-title">
                                    Praesent elementum
                                    <br />hendrerit tortor.
                                </h3>

                                <a href="javascript:;" class="banner-link"
                                    >Click here</a
                                >
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="banner">
                            <a href="javascript:;">
                                <img
                                    v-lazy="'./images/banners/banner-2.jpg'"
                                    alt="Banner"
                                    width="570"
                                    height="280"
                                />
                            </a>

                            <div class="banner-content">
                                <h4 class="banner-subtitle">
                                    Quisque a lectus
                                </h4>

                                <h3 class="banner-title">
                                    Donec consectetuer
                                    <br />ligula vulputate.
                                </h3>

                                <a href="javascript:;" class="banner-link"
                                    >Click here</a
                                >
                            </div>
                        </div>
                    </div>
                </div>

                <hr class="mb-4" />

                <h2 class="title text-center mb-3">3 Columns</h2>

                <div class="row justify-content-center">
                    <div class="col-md-6 col-lg-4">
                        <div class="banner">
                            <a href="javascript:;">
                                <img
                                    v-lazy="
                                        './images/banners/3cols/banner-1.jpg'
                                    "
                                    :key="'./images/banners/3cols/banner-1.jpg'"
                                    alt="Banner"
                                    width="370"
                                    height="220"
                                />
                            </a>

                            <div class="banner-content">
                                <h4 class="banner-subtitle">
                                    Quisque a lectus
                                </h4>

                                <h3 class="banner-title">
                                    Praesent elementum
                                    <br />hendrerit tortor.
                                </h3>

                                <a href="javascript:;" class="banner-link"
                                    >Click here</a
                                >
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6 col-lg-4">
                        <div class="banner">
                            <a href="javascript:;">
                                <img
                                    v-lazy="
                                        './images/banners/3cols/banner-2.jpg'
                                    "
                                    :key="'./images/banners/3cols/banner-2.jpg'"
                                    alt="Banner"
                                    width="370"
                                    height="220"
                                />
                            </a>

                            <div class="banner-content">
                                <h4 class="banner-subtitle">
                                    Quisque a lectus
                                </h4>

                                <h3 class="banner-title">
                                    Donec consectetuer
                                    <br />ligula vulputate.
                                </h3>

                                <a href="javascript:;" class="banner-link"
                                    >Click here</a
                                >
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6 col-lg-4">
                        <div class="banner">
                            <a href="javascript:;">
                                <img
                                    v-lazy="
                                        './images/banners/3cols/banner-3.jpg'
                                    "
                                    :key="'./images/banners/3cols/banner-3.jpg'"
                                    alt="Banner"
                                    width="370"
                                    height="220"
                                />
                            </a>

                            <div class="banner-content">
                                <h4 class="banner-subtitle">
                                    Quisque a lectus
                                </h4>

                                <h3 class="banner-title text-white">
                                    Phasellus ultrices
                                    <br />nulla quisnibh.
                                </h3>

                                <a href="javascript:;" class="banner-link"
                                    >Click here</a
                                >
                            </div>
                        </div>
                    </div>
                </div>

                <hr class="mb-4" />
            </div>

            <div class="container-fluid">
                <h2 class="title text-center mb-3">3 Columns Fullwidth</h2>

                <div class="row justify-content-center">
                    <div class="col-md-6 col-lg-4">
                        <div class="banner">
                            <a href="javascript:;">
                                <img
                                    v-lazy="
                                        './images/banners/3cols/banner-4.jpg'
                                    "
                                    :key="'./images/banners/3cols/banner-4.jpg'"
                                    alt="Banner"
                                    width="600"
                                    height="280"
                                />
                            </a>

                            <div class="banner-content">
                                <h4 class="banner-subtitle">
                                    Quisque a lectus
                                </h4>

                                <h3 class="banner-title">
                                    Praesent elementum
                                    <br />hendrerit tortor.
                                </h3>

                                <a href="javascript:;" class="banner-link"
                                    >Click here</a
                                >
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6 col-lg-4">
                        <div class="banner">
                            <a href="javascript:;">
                                <img
                                    v-lazy="
                                        './images/banners/3cols/banner-5.jpg'
                                    "
                                    alt="Banner"
                                    width="600"
                                    height="280"
                                />
                            </a>

                            <div class="banner-content">
                                <h4 class="banner-subtitle">
                                    Quisque a lectus
                                </h4>

                                <h3 class="banner-title">
                                    Donec consectetuer
                                    <br />ligula vulputate.
                                </h3>

                                <a href="javascript:;" class="banner-link"
                                    >Click here</a
                                >
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6 col-lg-4">
                        <div class="banner">
                            <a href="javascript:;">
                                <img
                                    v-lazy="
                                        './images/banners/3cols/banner-6.jpg'
                                    "
                                    alt="Banner"
                                    width="600"
                                    height="280"
                                />
                            </a>

                            <div class="banner-content">
                                <h4 class="banner-subtitle">
                                    Quisque a lectus
                                </h4>

                                <h3 class="banner-title text-white">
                                    Phasellus ultrices
                                    <br />nulla quisnibh.
                                </h3>

                                <a href="javascript:;" class="banner-link"
                                    >Click here</a
                                >
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="container">
                <hr class="mb-4" />

                <h2 class="title text-center mb-3">Large Banner</h2>

                <div class="row">
                    <div class="col-12">
                        <div class="banner banner-big">
                            <a href="javascript:;">
                                <img
                                    v-lazy="
                                        './images/banners/banner-fullwidth.jpg'
                                    "
                                    alt="Banner"
                                    width="1170"
                                    height="470"
                                />
                            </a>

                            <div class="banner-content">
                                <h4 class="banner-subtitle text-primary">
                                    Quisque a lectus
                                </h4>

                                <h3 class="banner-title text-white">
                                    Morbi interdum
                                    <br />mollis sapien.
                                </h3>

                                <p class="d-none d-lg-block">
                                    Lorem ipsum dolor sit amet, consectetuer
                                    adipiscing elit. Donec odio. Quisque
                                    volutpat mattis eros. Nullam malesuada erat
                                    ut turpis. Suspendisse urna nibh, viverra
                                    non, posuere a, pede.
                                </p>

                                <a
                                    href="javascript:;"
                                    class="btn btn-primary btn-rounded"
                                >
                                    <span>Click Here</span>
                                    <i class="icon-long-arrow-right"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>

                <hr class="mb-4" />

                <h2 class="title text-center mb-3">Grid (3 Banners)</h2>

                <div class="row">
                    <div class="col-lg-8">
                        <div class="banner banner-big">
                            <a href="javascript:;">
                                <img
                                    v-lazy="
                                        './images/banners/grid/3cols/banner-1.jpg'
                                    "
                                    alt="Banner"
                                    width="770"
                                    height="470"
                                />
                            </a>

                            <div class="banner-content">
                                <h4 class="banner-subtitle">
                                    Quisque a lectus
                                </h4>

                                <h3 class="banner-title">
                                    Morbi in sem
                                    <br />placerat.
                                </h3>

                                <a
                                    href="javascript:;"
                                    class="btn btn-primary btn-rounded"
                                >
                                    <span>Click Here</span>
                                    <i class="icon-long-arrow-right"></i>
                                </a>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-4">
                        <div class="row">
                            <div class="col-sm-6 col-lg-12">
                                <div class="banner">
                                    <a href="javascript:;">
                                        <img
                                            v-lazy="
                                                './images/banners/grid/3cols/banner-2.jpg'
                                            "
                                            alt="Banner"
                                            width="370"
                                            height="222"
                                        />
                                    </a>

                                    <div class="banner-content">
                                        <h4 class="banner-subtitle">
                                            Quisque a lectus
                                        </h4>

                                        <h3 class="banner-title text-white">
                                            Donec consectetuer
                                            <br />ligula vulputate.
                                        </h3>

                                        <a
                                            href="javascript:;"
                                            class="banner-link"
                                            >Click here</a
                                        >
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-6 col-lg-12">
                                <div class="banner">
                                    <a href="javascript:;">
                                        <img
                                            v-lazy="
                                                './images/banners/grid/3cols/banner-3.jpg'
                                            "
                                            width="370"
                                            height="222"
                                            alt="Banner"
                                        />
                                    </a>

                                    <div class="banner-content">
                                        <h4 class="banner-subtitle">
                                            Quisque a lectus
                                        </h4>

                                        <h3 class="banner-title">
                                            Phasellus
                                            <br />ultrices nulla.
                                        </h3>

                                        <a
                                            href="javascript:;"
                                            class="banner-link"
                                            >Click here</a
                                        >
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <hr class="mb-4" />

                <h2 class="title text-center mb-3">Grid (4 Banners)</h2>

                <div class="row justify-content-center">
                    <div class="col-sm-6 col-lg-4">
                        <div class="banner">
                            <a href="javascript:;">
                                <img
                                    v-lazy="
                                        './images/banners/grid/4cols/banner-1.jpg'
                                    "
                                    width="370"
                                    height="550"
                                    alt="Banner"
                                />
                            </a>

                            <div class="banner-content">
                                <h4 class="banner-subtitle">
                                    Quisque a lectus
                                </h4>

                                <h3 class="banner-title">
                                    Phasellus
                                    <br />ultrices nulla.
                                </h3>

                                <a href="javascript:;" class="banner-link"
                                    >Click here</a
                                >
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-6 col-lg-4 order-lg-last">
                        <div class="banner">
                            <a href="javascript:;">
                                <img
                                    v-lazy="
                                        './images/banners/grid/4cols/banner-4.jpg'
                                    "
                                    width="370"
                                    height="550"
                                    alt="Banner"
                                />
                            </a>

                            <div class="banner-content">
                                <h4 class="banner-subtitle">
                                    Quisque a lectus
                                </h4>

                                <h3 class="banner-title text-white">
                                    Donec consectetuer
                                    <br />ligula vulputate.
                                </h3>

                                <a href="javascript:;" class="banner-link"
                                    >Click here</a
                                >
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-4">
                        <div class="row">
                            <div class="col-sm-6 col-lg-12">
                                <div class="banner">
                                    <a href="javascript:;">
                                        <img
                                            v-lazy="
                                                './images/banners/grid/4cols/banner-2.jpg'
                                            "
                                            width="370"
                                            height="265"
                                            alt="Banner"
                                        />
                                    </a>

                                    <div class="banner-content">
                                        <h4 class="banner-subtitle">
                                            Quisque a lectus
                                        </h4>

                                        <h3 class="banner-title">
                                            Phasellus
                                            <br />ultrices nulla.
                                        </h3>

                                        <a
                                            href="javascript:;"
                                            class="banner-link"
                                            >Click here</a
                                        >
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-6 col-lg-12">
                                <div class="banner">
                                    <a href="javascript:;">
                                        <img
                                            v-lazy="
                                                './images/banners/grid/4cols/banner-3.jpg'
                                            "
                                            width="370"
                                            height="265"
                                            alt="Banner"
                                        />
                                    </a>

                                    <div class="banner-content">
                                        <h4 class="banner-subtitle">
                                            Quisque a lectus
                                        </h4>

                                        <h3 class="banner-title text-white">
                                            Donec consectetuer
                                            <br />ligula vulputate.
                                        </h3>

                                        <a
                                            href="javascript:;"
                                            class="banner-link"
                                            >Click here</a
                                        >
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
    }
};
</script>
