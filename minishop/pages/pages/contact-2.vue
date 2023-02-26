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
                    <li class="breadcrumb-item active">Contact us 2</li>
                </ol>
            </div>
        </nav>

        <div class="page-content">
            <div id="map" class="mb-5">
                <gmap-map
                :center="{lat:10, lng:10}"
                :zoom="7"
                map-type-id="terrain"
                style="width: 500px; height: 300px"
                ></gmap-map>
            </div>
            <!-- End #map -->
            <div class="container">
                <div class="row">
                    <div class="col-md-4">
                        <div class="contact-box text-center">
                            <h3>Office</h3>

                            <address>
                                1 New York Plaza, New York,
                                <br />NY 10004, USA
                            </address>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="contact-box text-center">
                            <h3>Start a Conversation</h3>

                            <div>
                                <a href="mailto:#">info@Molla.com</a>
                            </div>
                            <div>
                                <a href="tel:#">+1 987-876-6543</a>,
                                <a href="tel:#">+1 987-976-1234</a>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="contact-box text-center">
                            <h3>Social</h3>

                            <div class="social-icons social-icons-color justify-content-center">
                                <a
                                    href="#"
                                    class="social-icon social-facebook"
                                    title="Facebook"
                                    target="_blank"
                                >
                                    <i class="icon-facebook-f"></i>
                                </a>
                                <a
                                    href="#"
                                    class="social-icon social-twitter"
                                    title="Twitter"
                                    target="_blank"
                                >
                                    <i class="icon-twitter"></i>
                                </a>
                                <a
                                    href="#"
                                    class="social-icon social-instagram"
                                    title="Instagram"
                                    target="_blank"
                                >
                                    <i class="icon-instagram"></i>
                                </a>
                                <a
                                    href="#"
                                    class="social-icon social-youtube"
                                    title="Youtube"
                                    target="_blank"
                                >
                                    <i class="icon-youtube"></i>
                                </a>
                                <a
                                    href="#"
                                    class="social-icon social-pinterest"
                                    title="Pinterest"
                                    target="_blank"
                                >
                                    <i class="icon-pinterest"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>

                <hr class="mt-3 mb-5 mt-md-1" />
                <div class="touch-container row justify-content-center">
                    <div class="col-md-9 col-lg-7">
                        <div class="text-center">
                            <h2 class="title mb-1">Get In Touch</h2>

                            <p
                                class="lead text-primary"
                            >We collaborate with ambitious brands and people; we’d love to build something great together.</p>

                            <p
                                class="mb-3"
                            >Vestibulum volutpat, lacus a ultrices sagittis, mi neque euismod dui, eu pulvinar nunc sapien ornare nisl. Phasellus pede arcu, dapibus eu, fermentum et, dapibus sed, urna.</p>
                        </div>

                        <form action="#" class="contact-form mb-2">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="cname" class="sr-only">Name</label>
                                    <input
                                        type="text"
                                        class="form-control"
                                        id="cname"
                                        placeholder="Name *"
                                        required
                                    />
                                </div>

                                <div class="col-sm-4">
                                    <label for="cemail" class="sr-only">Name</label>
                                    <input
                                        type="email"
                                        class="form-control"
                                        id="cemail"
                                        placeholder="Email *"
                                        required
                                    />
                                </div>

                                <div class="col-sm-4">
                                    <label for="cphone" class="sr-only">Phone</label>
                                    <input
                                        type="tel"
                                        class="form-control"
                                        id="cphone"
                                        placeholder="Phone"
                                    />
                                </div>
                            </div>

                            <label for="csubject" class="sr-only">Subject</label>
                            <input
                                type="text"
                                class="form-control"
                                id="csubject"
                                placeholder="Subject"
                            />

                            <label for="cmessage" class="sr-only">Message</label>
                            <textarea
                                class="form-control"
                                cols="30"
                                rows="4"
                                id="cmessage"
                                required
                                placeholder="Message *"
                            ></textarea>

                            <div class="text-center">
                                <button
                                    type="submit"
                                    class="btn btn-outline-primary-2 btn-minwidth-sm"
                                >
                                    <span>SUBMIT</span>
                                    <i class="icon-long-arrow-right"></i>
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
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