<template>
    <main class="main">
        <page-header title="Portfolio" subtitle="Elements"></page-header>

        <div
            class="page-content"
            v-images-loaded.on="handleIsotope"
            :style="loaded ? '' : 'visiblity: hidden; opacity: 0'"
        >
            <div class="container">
                <h2 class="title text-center mb-2">Grid 3 Columns</h2>

                <nav class="portfolio-nav">
                    <ul
                        class="nav-filter portfolio-filter justify-content-center"
                    >
                        <li class="active">
                            <a
                                href="#"
                                @click.prevent="
                                    isotopeFilter('iso1', 'isAll', $event)
                                "
                                >All</a
                            >
                        </li>
                        <li>
                            <a
                                href="#"
                                @click.prevent="
                                    isotopeFilter('iso1', 'isWomen', $event)
                                "
                                >Women</a
                            >
                        </li>
                        <li>
                            <a
                                href="#"
                                @click.prevent="
                                    isotopeFilter('iso1', 'isMen', $event)
                                "
                                >Men</a
                            >
                        </li>
                        <li>
                            <a
                                href="#"
                                @click.prevent="
                                    isotopeFilter(
                                        'iso1',
                                        'isAccessories',
                                        $event
                                    )
                                "
                                >Accessories</a
                            >
                        </li>
                    </ul>
                </nav>

                <isotope
                    ref="iso1"
                    class="portfolio-container"
                    :list="portfolio1"
                    :options="isotopeOptions"
                >
                    <portfolio-one
                        class="portfolio-item col-sm-6 col-lg-4"
                        :class="item.class"
                        v-for="(item, index) in portfolio1"
                        :image="item.image"
                        :category="item.category"
                        :key="index"
                    ></portfolio-one>

                    <div
                        class="grid-sizer col-sm-6 col-lg-4"
                        key="grid-sizer"
                    ></div>
                </isotope>

                <hr class="mb-4" />

                <h2 class="title text-center mb-2">Grid 4 Columns</h2>

                <nav class="portfolio-nav">
                    <ul
                        class="nav-filter portfolio-filter justify-content-center"
                    >
                        <li class="active">
                            <a
                                href="#"
                                @click.prevent="
                                    isotopeFilter('iso2', 'isAll', $event)
                                "
                                >All</a
                            >
                        </li>
                        <li>
                            <a
                                href="#"
                                @click.prevent="
                                    isotopeFilter('iso2', 'isWomen', $event)
                                "
                                >Women</a
                            >
                        </li>
                        <li>
                            <a
                                href="#"
                                @click.prevent="
                                    isotopeFilter('iso2', 'isMen', $event)
                                "
                                >Men</a
                            >
                        </li>
                        <li>
                            <a
                                href="#"
                                @click.prevent="
                                    isotopeFilter(
                                        'iso2',
                                        'isAccessories',
                                        $event
                                    )
                                "
                                >Accessories</a
                            >
                        </li>
                    </ul>
                </nav>

                <isotope
                    ref="iso2"
                    class="portfolio-container"
                    :list="portfolio2"
                    :options="isotopeOptions"
                >
                    <portfolio-one
                        class="portfolio-item col-sm-6 col-md-4 col-lg-3"
                        :class="item.class"
                        v-for="(item, index) in portfolio2"
                        :image="item.image"
                        :category="item.category"
                        :key="index"
                    ></portfolio-one>

                    <div
                        class="grid-sizer col-sm-6 col-md-4 col-lg-3"
                        key="grid-sizer"
                    ></div>
                </isotope>

                <hr class="mb-4" />

                <h2 class="title text-center mb-2">Masonry 3 Columns</h2>

                <nav class="portfolio-nav">
                    <ul
                        class="nav-filter portfolio-filter justify-content-center"
                    >
                        <li class="active">
                            <a
                                href="#"
                                @click.prevent="
                                    isotopeFilter('iso3', 'isAll', $event)
                                "
                                >All</a
                            >
                        </li>
                        <li>
                            <a
                                href="#"
                                @click.prevent="
                                    isotopeFilter('iso3', 'isWomen', $event)
                                "
                                >Women</a
                            >
                        </li>
                        <li>
                            <a
                                href="#"
                                @click.prevent="
                                    isotopeFilter('iso3', 'isMen', $event)
                                "
                                >Men</a
                            >
                        </li>
                        <li>
                            <a
                                href="#"
                                @click.prevent="
                                    isotopeFilter(
                                        'iso3',
                                        'isAccessories',
                                        $event
                                    )
                                "
                                >Accessories</a
                            >
                        </li>
                    </ul>
                </nav>

                <isotope
                    ref="iso3"
                    class="portfolio-container"
                    :list="portfolio3"
                    :options="isotopeOptions"
                >
                    <portfolio-one
                        class="portfolio-item col-sm-6 col-lg-4"
                        :class="item.class"
                        v-for="(item, index) in portfolio3"
                        :image="item.image"
                        :category="item.category"
                        :key="index"
                    ></portfolio-one>

                    <div
                        class="grid-sizer col-sm-6 col-lg-4"
                        key="grid-sizer"
                    ></div>
                </isotope>

                <hr class="mb-4" />

                <h2 class="title text-center mb-2">Masonry 4 Columns</h2>

                <nav class="portfolio-nav">
                    <ul
                        class="nav-filter portfolio-filter justify-content-center"
                    >
                        <li class="active">
                            <a
                                href="#"
                                @click.prevent="
                                    isotopeFilter('iso4', 'isAll', $event)
                                "
                                >All</a
                            >
                        </li>
                        <li>
                            <a
                                href="#"
                                @click.prevent="
                                    isotopeFilter('iso4', 'isWomen', $event)
                                "
                                >Women</a
                            >
                        </li>
                        <li>
                            <a
                                href="#"
                                @click.prevent="
                                    isotopeFilter('iso4', 'isMen', $event)
                                "
                                >Men</a
                            >
                        </li>
                        <li>
                            <a
                                href="#"
                                @click.prevent="
                                    isotopeFilter(
                                        'iso4',
                                        'isAccessories',
                                        $event
                                    )
                                "
                                >Accessories</a
                            >
                        </li>
                    </ul>
                </nav>

                <isotope
                    ref="iso4"
                    class="portfolio-container"
                    :list="portfolio4"
                    :options="isotopeOptions"
                >
                    <portfolio-one
                        class="portfolio-item col-sm-6 col-md-4 col-lg-3"
                        :class="item.class"
                        v-for="(item, index) in portfolio4"
                        :image="item.image"
                        :category="item.category"
                        :key="index"
                    ></portfolio-one>

                    <div
                        class="grid-sizer col-sm-6 col-md-4 col-lg-3"
                        key="grid-sizer"
                    ></div>
                </isotope>

                <hr class="mb-4" />
            </div>

            <div class="container-fluid">
                <h2 class="title text-center mb-2">
                    Fullwidth with Text
                    <span class="title-separator">(No space)</span>
                </h2>

                <nav class="portfolio-nav">
                    <ul
                        class="nav-filter portfolio-filter justify-content-center"
                    >
                        <li class="active">
                            <a
                                href="#"
                                @click.prevent="
                                    isotopeFilter('iso5', 'isAll', $event)
                                "
                                >All</a
                            >
                        </li>
                        <li>
                            <a
                                href="#"
                                @click.prevent="
                                    isotopeFilter('iso5', 'isWomen', $event)
                                "
                                >Women</a
                            >
                        </li>
                        <li>
                            <a
                                href="#"
                                @click.prevent="
                                    isotopeFilter('iso5', 'isMen', $event)
                                "
                                >Men</a
                            >
                        </li>
                        <li>
                            <a
                                href="#"
                                @click.prevent="
                                    isotopeFilter(
                                        'iso5',
                                        'isAccessories',
                                        $event
                                    )
                                "
                                >Accessories</a
                            >
                        </li>
                    </ul>
                </nav>

                <isotope
                    ref="iso5"
                    class="portfolio-container portfolio-nogap"
                    :list="portfolio5"
                    :options="isotopeOptions"
                >
                    <portfolio-two
                        class="portfolio-item col-sm-6 col-md-4 col-lg-3"
                        :class="item.class"
                        v-for="(item, index) in portfolio5"
                        :image="item.image"
                        :category="item.category"
                        :key="index"
                    ></portfolio-two>

                    <div
                        class="grid-sizer col-sm-6 col-md-4 col-lg-3"
                        key="grid-sizer"
                    ></div>
                </isotope>
            </div>
        </div>

        <element-list></element-list>
    </main>
</template>

<script>
import isotope from 'vueisotope';
import imagesLoaded from 'vue-images-loaded';
import PageHeader from '~/components/elements/PageHeader';
import PortfolioOne from '~/components/elements/portfolio/PortfolioOne';
import PortfolioTwo from '~/components/elements/portfolio/PortfolioTwo';
import ElementList from '~/components/partial/elements/ElementList';
import {
    portfolio1,
    portfolio2,
    portfolio3,
    portfolio4,
    portfolio5
} from '~/utilities/data';

export default {
    components: {
        isotope,
        PageHeader,
        PortfolioOne,
        PortfolioTwo,
        ElementList
    },
    directives: {
        imagesLoaded
    },
    data: function() {
        return {
            isotopeOptions: {
                itemSelector: 'portfolio-item',
                layoutMode: 'masonry',
                percentPosition: false,
                masonry: {
                    columnWidth: '.grid-sizer'
                },
                getFilterData: {
                    isMen: function(elem) {
                        if (elem) {
                            return elem.class.split(' ').includes('men');
                        } else return false;
                    },
                    isWomen: function(elem) {
                        if (elem) {
                            return elem.class.split(' ').includes('women');
                        } else return false;
                    },
                    isAccessories: function(elem) {
                        if (elem) {
                            return elem.class
                                .split(' ')
                                .includes('accessories');
                        } else return false;
                    },
                    isAll: function(elem) {
                        return true;
                    }
                }
            },
            loaded: false,
            portfolio1: portfolio1,
            portfolio2: portfolio2,
            portfolio3: portfolio3,
            portfolio4: portfolio4,
            portfolio5: portfolio5
        };
    },
    methods: {
        handleIsotope: function() {
            for (let iso in this.$refs) {
                if (this.$refs[iso]) this.$refs[iso].layout('masonry');
            }
            this.loaded = true;
        },
        isotopeFilter: function(iso, filterClass, e) {
            this.$refs[iso].filter(filterClass);
            let prevActive = e.currentTarget.parentElement.parentElement.querySelector(
                '.active'
            );
            if (prevActive) {
                prevActive.classList.remove('active');
            }

            e.currentTarget.parentElement.classList.add('active');
        }
    }
};
</script>
