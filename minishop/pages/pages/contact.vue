<template>
    <main class="main">
        <nav class="breadcrumb-nav border-0 mb-0">
            <div class="container">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <nuxt-link to="/">Home</nuxt-link>
                    </li>
                    <li class="breadcrumb-item">
                        <a href="javascript:;">Pages</a>
                    </li>
                    <li class="breadcrumb-item active">Contact us</li>
                </ol>
            </div>
        </nav>

        <div class="container">
            <div
                class="page-header page-header-big text-center"
                v-lazy:background-image="'./images/contact-header-bg.jpg'"
            >
                <h1 class="page-title text-white">
                    Contact us
                    <span class="text-white">keep in touch with us</span>
                </h1>
            </div>
        </div>

        <div class="page-content pb-0">
            <div class="container">
                <div class="row">
                    <div class="col-lg-6 mb-2 mb-lg-0">
                        <h2 class="title mb-1">Contact Information</h2>

                        <p
                            class="mb-3"
                        >Vestibulum volutpat, lacus a ultrices sagittis, mi neque euismod dui, eu pulvinar nunc sapien ornare nisl. Phasellus pede arcu, dapibus eu, fermentum et, dapibus sed, urna.</p>
                        <div class="row">
                            <div class="col-sm-7">
                                <div class="contact-info">
                                    <h3>The Office</h3>

                                    <ul class="contact-list">
                                        <li>
                                            <i class="icon-map-marker"></i>
                                            70 Washington Square South New York, NY 10012, United States
                                        </li>
                                        <li>
                                            <i class="icon-phone"></i>
                                            <a href="tel:#">+92 423 567</a>
                                        </li>
                                        <li>
                                            <i class="icon-envelope"></i>
                                            <a href="mailto:#">info@Molla.com</a>
                                        </li>
                                    </ul>
                                </div>
                            </div>

                            <div class="col-sm-5">
                                <div class="contact-info">
                                    <h3>The Office</h3>

                                    <ul class="contact-list">
                                        <li>
                                            <i class="icon-clock-o"></i>
                                            <span class="text-dark">Monday-Saturday</span>
                                            <br />11am-7pm ET
                                        </li>
                                        <li>
                                            <i class="icon-calendar"></i>
                                            <span class="text-dark">Sunday</span>
                                            <br />11am-6pm ET
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-6">
                        <h2 class="title mb-1">Got Any Questions?</h2>

                        <p class="mb-2">Use the form below to get in touch with the sales team</p>

                        <form action="#" class="contact-form mb-3">
                            <div class="row">
                                <div class="col-sm-6">
                                    <label for="cname" class="sr-only">Name</label>
                                    <input
                                        type="text"
                                        class="form-control"
                                        id="cname"
                                        placeholder="Name *"
                                        required
                                    />
                                </div>

                                <div class="col-sm-6">
                                    <label for="cemail" class="sr-only">Email</label>
                                    <input
                                        type="email"
                                        class="form-control"
                                        id="cemail"
                                        placeholder="Email *"
                                        required
                                    />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-6">
                                    <label for="cphone" class="sr-only">Phone</label>
                                    <input
                                        type="tel"
                                        class="form-control"
                                        id="cphone"
                                        placeholder="Phone"
                                    />
                                </div>

                                <div class="col-sm-6">
                                    <label for="csubject" class="sr-only">Subject</label>
                                    <input
                                        type="text"
                                        class="form-control"
                                        id="csubject"
                                        placeholder="Subject"
                                    />
                                </div>
                            </div>

                            <label for="cmessage" class="sr-only">Message</label>
                            <textarea
                                class="form-control"
                                cols="30"
                                rows="4"
                                id="cmessage"
                                required
                                placeholder="Message *"
                            ></textarea>

                            <button type="submit" class="btn btn-outline-primary-2 btn-minwidth-sm">
                                <span>SUBMIT</span>
                                <i class="icon-long-arrow-right"></i>
                            </button>
                        </form>
                    </div>
                </div>

                <hr class="mt-4 mb-5" />

                <div class="stores mb-4 mb-lg-5">
                    <h2 class="title text-center mb-3">Our Stores</h2>

                    <div class="row">
                        <div class="col-lg-6">
                            <div class="store">
                                <div class="row">
                                    <div class="col-sm-5 col-xl-6">
                                        <figure class="store-media mb-2 mb-lg-0">
                                            <img v-lazy="'./images/stores/img-1.jpg'" class="w-100" alt="image" />
                                        </figure>
                                    </div>

                                    <div class="col-sm-7 col-xl-6">
                                        <div class="store-content">
                                            <h3 class="store-title">Wall Street Plaza</h3>

                                            <address>88 Pine St, New York, NY 10005, USA</address>
                                            <div>
                                                <a href="tel:#">+1 987-876-6543</a>
                                            </div>

                                            <h4 class="store-subtitle">Store Hours:</h4>

                                            <div>Monday - Saturday 11am to 7pm</div>
                                            <div>Sunday 11am to 6pm</div>

                                            <a
                                                href="javascript:;"
                                                class="btn btn-link"
                                                target="_blank"
                                            >
                                                <span>View Map</span>
                                                <i class="icon-long-arrow-right"></i>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-6">
                            <div class="store">
                                <div class="row">
                                    <div class="col-sm-5 col-xl-6">
                                        <figure class="store-media mb-2 mb-lg-0">
                                            <img v-lazy="'./images/stores/img-2.jpg'" class="w-100" alt="image" />
                                        </figure>
                                    </div>

                                    <div class="col-sm-7 col-xl-6">
                                        <div class="store-content">
                                            <h3 class="store-title">One New York Plaza</h3>

                                            <address>88 Pine St, New York, NY 10005, USA</address>
                                            <div>
                                                <a href="tel:#">+1 987-876-6543</a>
                                            </div>

                                            <h4 class="store-subtitle">Store Hours:</h4>

                                            <div>Monday - Friday 9am to 8pm</div>
                                            <div>Saturday - 9am to 2pm</div>
                                            <div>Sunday - Closed</div>

                                            <a
                                                href="javascript:;"
                                                class="btn btn-link"
                                                target="_blank"
                                            >
                                                <span>View Map</span>
                                                <i class="icon-long-arrow-right"></i>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="map">
                <gmap-map
                    :center="{lat:10, lng:10}"
                    :zoom="7"
                    map-type-id="terrain"
                    style="width: 500px; height: 300px"
                ></gmap-map>
            </div>
            <!-- End #map -->
        </div>
    </main>
</template>

<script>
import Vue from 'vue'
import * as VueGoogleMaps from 'vue2-google-maps'
Vue.use(VueGoogleMaps, {
  load: {
    key: 'AIzaSyBzlLYISGjL_ovJwAehh6ydhB56fCCpPQw',
    libraries: 'places', // This is required if you use the Autocomplete plugin
    // OR: libraries: 'places,drawing'
    // OR: libraries: 'places,drawing,visualization'
    // (as you require)
  }
})
export default {};
</script>